import random

def GenerateSup(filename):
    fr = open(filename + ".txt", "r", encoding='utf-8', newline='')
    fw = open(filename + ".sql", "w", encoding='utf-8')
    for line in fr:
        fw.write("INSERT INTO {0} (name_{0}) VALUES (\'{1}\');\n".format(filename, line.rstrip()))

GenerateSup("area")
GenerateSup("lang")
GenerateSup("own")
GenerateSup("city")
GenerateSup("binding")
GenerateSup("style")

#Генерация author
fr = open("author.txt", "r", encoding='utf-8')
fw = open("author.sql", "w", encoding='utf-8')
for line in fr:
    author = line.split(" ")
    fw.write("INSERT INTO author (second_name_author, name_author, last_name_author) VALUES ('{0}', '{1}', '{2}');\n".format(author[0], author[1], author[2].rstrip()))


