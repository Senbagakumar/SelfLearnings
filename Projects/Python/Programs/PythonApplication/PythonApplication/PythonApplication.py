# https://docs.microsoft.com/en-us/visualstudio/python/tutorial-working-with-python-in-visual-studio-step-03-interactive-repl?view=vs-2019

#https://docs.microsoft.com/en-us/visualstudio/python/learn-django-in-visual-studio-step-01-project-and-solution?view=vs-2019

print("Hellow World");

import sys 
from math import cos, radians

#for i in range(360):
#	print(cos(radians(i)))

#def make_dot_string(num):
#	return '' * int(20 * cos(radians(num)) + 20) + 'o'


#def make_dot_string(x):
#    rad = radians(x)                             # cos works with radians
#    numspaces = int(20 * cos(radians(x)) + 20)   # scale to 0-40 spaces
#    st = ' ' * numspaces + 'o'                   # place 'o' after the spaces
#    return st

#def main():
#    for i in range(0, 1800, 12):
#        s = make_dot_string(i)
#        print(s)

#main()

import numpy as np
import matplotlib.pyplot as plt

def main():
	x=np.arange(0, radians(1800), radians(12))
	plt.plot(x,np.cos(x),'b')
	plt.show()

main();

