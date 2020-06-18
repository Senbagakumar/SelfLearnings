thislist = ["apple", "banana", "cherry", "orange", "kiwi", "melon", "mango"]
print(thislist)
print(thislist[1])
print(thislist[-1])
print(thislist[2:5])
print(thislist[:4])
print(thislist[2:])
print(thislist[-4:-1])
thislist[1]='blackcurrant'
print(thislist)
thislist.reverse()
print(thislist)
for x in thislist:
	print(x)
thislist.pop()
print(thislist)
thislist.remove('mango');
print(thislist)
del thislist[0]
print(thislist)
