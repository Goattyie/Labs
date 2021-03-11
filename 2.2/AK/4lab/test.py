import os
os.system('color f0')


A = (1,1,1,1,1,1,1,1,1,1,1,1,1)
B = (2,2,2,2,2,2,2,2)

print("A => {}".format(A))
print("B => {}".format(B))
Y = 0
for k in range(1,7):
	try:
		Y = Y + k**2 * A[k]**2 - A[2*k] / (A[6 + 1 - k] - B[k + 1])
		print("Результат {}-й итерации: {}".format(k,int(Y)))
	except Exception as e:
		print("Деление на 0")
		break

print(int(Y))

A = (1,2,3,4,5,6,7,8,9,10,11,12,13)
B = (1,2,3,4,5,6,7,8)

print("A => {}".format(A))
print("B => {}".format(B))
Y = 0
for k in range(1,7):
	try:
		Y = Y + k**2 * A[k]**2 - A[2*k] / (A[6 + 1 - k] - B[k + 1])
		print("Результат {}-й итерации: {}".format(k,int(Y)))
	except Exception as e:
		print("Деление на 0")
		break
print(int(Y))

A = (10,12,33,44,125,516,67,18,29,510,111,212,513)
B = (22,25,12,41,51,63,75,83)

print("A => {}".format(A))
print("B => {}".format(B))
Y = 0
for k in range(1,7):
	try:
		Y = Y + k**2 * A[k]**2 - A[2*k] / (A[6 + 1 - k] - B[k + 1])
		print("Результат {}-й итерации: {}".format(k,int(Y)))
	except Exception as e:
		print("Деление на 0")
		break

print(Y)