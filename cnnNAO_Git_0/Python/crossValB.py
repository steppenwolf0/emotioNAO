# These are all the modules we'll be using later. Make sure you can import them
# before proceeding further.
from __future__ import print_function
import numpy as np
import os
import sys
import tarfile
import math
import random
import sys
import os
import struct
import numpy as np
import matplotlib.pyplot 
from IPython.display import display, Image
from scipy import ndimage
from six.moves.urllib.request import urlretrieve
from six.moves import cPickle as pickle
from funcCNN import *

"""
Loosely inspired by http://abel.ee.ucla.edu/cvxopt/_downloads/mnist.py
which is GPL licensed.
"""
def show(image):
    """
    Render a given numpy.uint8 2D array of pixel data.
    """
    from matplotlib import pyplot
    import matplotlib as mpl
    fig = pyplot.figure()
    ax = fig.add_subplot(1,1,1)
    imgplot = ax.imshow(image, cmap=mpl.cm.Greys)
    imgplot.set_interpolation('nearest')
    ax.xaxis.set_ticks_position('top')
    ax.yaxis.set_ticks_position('left')
    pyplot.show()
	
def crossValB_10_Generic(indexVar,labelSize,inputChannels):
	size0=inputChannels

	data=[]
	labels=[]
	
	#load training data
	training_data=list(read(dataset='training', path='/home/alejandro/Desktop/multi/data/'))
	print(len(training_data))
	#label, pixels = training_data[59999]
	#print(label)
	#print(pixels.shape)
	#show(pixels)
	
	for i in range (0,60000):
		label, pixels = training_data[i]
		data.append(pixels)
		labels.append(label)

	

	#load testing data
	testing_data=list(read(dataset='testing', path='/home/alejandro/Desktop/multi/data/'))
	print(len(testing_data))
	
	for i in range (0,10000):
		label, pixels = testing_data[i]
		data.append(pixels)
		labels.append(label)
	data=np.array(data)
	print('data set', data.shape)
	#8
	#show(data[59999])
	#3
   	#show(data[10])
	#0
   	#show(data[60750])
    

	testIndex=openVector('./index/'+str(indexVar)+'test_index.txt')
	valIndex=openVector('./index/'+str(indexVar)+'val_index.txt')
	trainIndex=openVector('./index/'+str(indexVar)+'train_index.txt')
	
	testIndex=testIndex.astype(int)
	valIndex=valIndex.astype(int)
	trainIndex=trainIndex.astype(int)
		
	train=[]
	test=[]
	valid=[]
	
	
	trainLabels=[]
	testLabels=[]
	validLabels=[]
	#train***************************************************************************	
	for i in range (0,len(trainIndex)):
		trainLabels.append(labels[trainIndex[i]])
		train.append(data[trainIndex[i]])
	#test***************************************************************************
	for i in range (0,len(testIndex)):
		testLabels.append(labels[testIndex[i]])
		test.append(data[testIndex[i]])
	#valid***************************************************************************
	for i in range (0,len(valIndex)):
		validLabels.append(labels[valIndex[i]])
		valid.append(data[valIndex[i]])
	
	train=np.array(train)
	trainLabels=np.array(trainLabels)
	

	test=np.array(test)
	testLabels=np.array(testLabels)

	valid=np.array(valid)
	validLabels=np.array(validLabels)
	
	print(train.shape)
	print(trainLabels.shape)
	print(valid.shape)
	print(validLabels.shape)
	print(test.shape)
	print(testLabels.shape)


	oneHot_train_labels=oneHot(trainLabels,labelSize)
	print(oneHot_train_labels.shape)
	
	#train_dataset_Flat=compressArray(train,vectorSize)
	#print(train_dataset_Flat.shape)

	oneHot_valid_labels=oneHot(validLabels,labelSize)
	print(oneHot_valid_labels.shape)

	#valid_dataset_Flat=compressArray(valid,vectorSize)
	#print(valid_dataset_Flat.shape)

	oneHot_test_labels=oneHot(testLabels,labelSize)
	print(oneHot_test_labels.shape)

	#test_dataset_Flat=compressArray(test,vectorSize)
	#print(test_dataset_Flat.shape)

	return(test,oneHot_test_labels,valid,oneHot_valid_labels,train,oneHot_train_labels)
