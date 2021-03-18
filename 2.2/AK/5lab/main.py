matrix = [[-1, 3, 45, 32, 15, 97, 15, 32], [0, 31, 11, 14, 23, 48, 12, 44], [3, -10, -100, 12, 30, 6, 7, 9], [10, 101, 12, 31, 512, 13, 12, 11], [-31, 12, -34, 134, 23, 12, 4, 9]]

min = 3223532
for item in matrix:
    max = item[0]
    for i in item:
        if max <= i:
            max = i
    if min >= max:
        min = max
print("Min value:", min)

