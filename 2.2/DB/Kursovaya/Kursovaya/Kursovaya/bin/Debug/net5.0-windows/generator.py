def Generate(name, filename):
    f = open(filename + ".txt", "r")
    for line in f:
        print(line + "\n")
    