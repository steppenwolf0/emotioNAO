#Declarations******************************************************************************
from __future__ import print_function
import numpy as np
import os
import sys
import tarfile
import math
import random
import sys
from IPython.display import display, Image
from scipy import ndimage
#from six.moves.urllib.request import urlretrieve
#from six.moves import cPickle as pickle
from funcCNN import *
#from crossValB import *
import matplotlib.pyplot 
from PIL import Image
import socket

import io
from array import array

#python gen0D.py 500 255 255 255 256 4 3 2 12 3 2 0 0 0

def getImage(im):
	outData=[]
	im = im.convert('L')
	processedIm = np.array(im)
	outData.append(processedIm)
	return np.array(outData)

#Parameters*******************************************************************************
HOST = '127.0.0.10'
PORT = 8888
ADDR = (HOST,PORT)
BUFSIZE = 30054

serv = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

serv.bind(ADDR)
serv.listen(5)
count=0
print ('listening ...')
#Parameters*******************************************************************************
#number of classes
labelSize=7
#fixed size of the data
vectorSize=100
vector2D=100
#maximum number of iterations
limit=1.00
#regularization on the weights
beta=0.0001

#Parameters*******************************************************************************
w1=255 
w2=255
w3=255
w4=256
h1=4
h2=3
h3=2
wd1=12
wd2=3
wd3=2

import tensorflow as tf
#declare interactive session
sess = tf.InteractiveSession()

#INPUT->CONV LAYER->CONV LAYER->CONV LAYER->RECT FLAT->RECT DROPOUT

#function to declare easily the weights only by shape
def weight_variable(shape):
	initial = tf.truncated_normal(shape, stddev=0.1)
	return tf.Variable(initial)
#function to declare easily the bias only by shape
def bias_variable(shape):
	initial = tf.constant(0.1, shape=shape)
	return tf.Variable(initial)

#input variable
x = tf.placeholder(tf.float32, [None, vectorSize,vector2D])
#keep probability to change from dropout 0.50 to 1.0 in validation and test
keep_prob = tf.placeholder(tf.float32)
#expected outputs variable

#arrange the tensor as an image (8*545) 1 channel
x_image0 = tf.reshape(x, [-1,vector2D,vectorSize,1])
x_image = tf.transpose(x_image0, perm=[0,3,2,1])
#arrange the tensor into 8 channels (1*545) 8 channels

#1 LAYER*************************************************************************************
#1 Convolutional Layer Explicit for regularization of the weights
#weigth first layer 8 input channels, 32 output channels, 1x4 filter window size
W_conv1 = weight_variable([wd1, wd1, vector2D, w1])
#bias declaration the size has to be the same as the output channels (32)
b_conv1 = bias_variable([w1])
#convolution (input weights) moving 1 step each time with a relu
h_conv1 = tf.nn.relu(tf.nn.conv2d(x_image, W_conv1, 
	strides=[1, h1, h1, 1], padding='SAME') + b_conv1)
#max pooling with a 4 width window size, moving 4 in width by step
h_pool1 = tf.nn.max_pool(h_conv1, ksize=[1, h1, h1, 1],
	strides=[1, h1, h1, 1], padding='SAME')
#output=545/4
#1 LAYER*************************************************************************************

#2 LAYER*************************************************************************************
#2 Convolutional Layer Explicit for regularization of the weights
#weigth first layer 32 input channels, 64 output channels, 1x4 filter window size
W_conv2 = weight_variable([wd2, wd2, w1, w2])
b_conv2 = bias_variable([w2])
#convolution (input weights) moving 1 step each time with a relu
h_conv2 = tf.nn.relu(tf.nn.conv2d(h_pool1, W_conv2, 
	strides=[1, h2, h2, 1], padding='SAME') + b_conv2)
#max pooling with a 4 width window size, moving 4 in width by step
h_pool2 = tf.nn.max_pool(h_conv2, ksize=[1, h2, h2, 1],
	strides=[1, h2, h2, 1], padding='SAME')
#output=545/16
#2 LAYER*************************************************************************************

#3 LAYER*************************************************************************************
#3 Convolutional Layer Explicit for regularization of the weights
#weigth first layer 64 input channels, 128 output channels, 1x4 filter window size
W_conv3 = weight_variable([wd3, wd3, w2, w3])
b_conv3 = bias_variable([w3])
#convolution (input weights) moving 1 step each time with a relu
h_conv3 = tf.nn.relu(tf.nn.conv2d(h_pool2, W_conv3, 
	strides=[1, h3,h3, 1], padding='SAME') + b_conv3)
#max pooling with a 4 width window size, moving 4 in width by step
h_pool3 = tf.nn.max_pool(h_conv3, ksize=[1, h3, h3, 1],
	strides=[1, h3, h3, 1], padding='SAME')
#output=545/64
#3 LAYER*************************************************************************************

#Rectifier LAYER*****************************************************************************	
#calculated coefficient for the flattening from the size of the 3 convolutional layer
coef=int (h_pool3.get_shape()[1]*h_pool3.get_shape()[2]*h_pool3.get_shape()[3]) 
h_pool2_flat = tf.reshape(h_pool3, [-1, coef])
#declare the weights considering the constants and 256 output 
W_fc1 = weight_variable([coef, w4])
b_fc1 = bias_variable([w4])

#rectifier (matmul)
h_fc1 = tf.nn.relu(tf.matmul(h_pool2_flat, W_fc1) + b_fc1)
#Rectifier LAYER*****************************************************************************

#Rectifier-Dropout LAYER**********************************************************************
#dropout
h_fc1_drop = tf.nn.dropout(h_fc1, keep_prob)
#declare weights with the ouput layer in this case 2 (labelSize)
W_fc2 = weight_variable([w4, labelSize])
b_fc2 = bias_variable([labelSize])
#output
y_conv = tf.matmul(h_fc1_drop, W_fc2) + b_fc2
#Rectifier-Dropout LAYER**********************************************************************

#softmax prediction remember we are using one hot labelss
#labels_prediction=tf.argmax(y_conv,1)
labels_prediction = tf.nn.softmax(tf.gather(y_conv, 0))
#start
sess.run(tf.initialize_all_variables())
# Add ops to save and restore all the variables.
saver = tf.train.Saver()
save_path = saver.restore(sess, "./model/model.ckpt")

while True:
	conn, addr = serv.accept()
	print ('client connected ... ', addr)
  
	while True:
		data = conn.recv(BUFSIZE)
		if not data: break
		image = Image.open(io.BytesIO(data))
		xa=getImage(image)
		prediction = labels_prediction.eval(feed_dict={x:xa, keep_prob: 1.0})
		count=count+1
		#conn.send(val)
		text=str(np.round(prediction, 3))+'\n'
		conn.send(str.encode(text))
		print(prediction)
	print ('finished writing file')
	conn.close()
print ('client disconnected')

sess.close()


































