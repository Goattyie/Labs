import random
from datetime import date as d

def GenerateSup(filename):
    fr = open(filename + ".txt", "r", encoding='utf-8', newline='')
    fw = open("sql/" + filename + ".sql", "w", encoding='utf-8')
    for line in fr:
        fw.write("INSERT INTO {0} (name_{0}) VALUES (\'{1}\');\n".format(filename, line.rstrip()))
    fw.close()
    fr.close()

GenerateSup("area")
GenerateSup("lang")
GenerateSup("own")
GenerateSup("city")
GenerateSup("binding")
GenerateSup("style")

#Генерация author
fr = open("author.txt", "r", encoding='utf-8')
fw = open("sql/author.sql", "w", encoding='utf-8')
for line in fr:
    author = line.split(" ")
    fw.write("INSERT INTO author (second_name_author, name_author, last_name_author) VALUES ('{0}', '{1}', '{2}');\n".format(author[0], author[1], author[2].rstrip()))
fw.close()
fr.close()

#узнать сколько строк в файле
def NodeCount(filename):
    line_count = 0
    with open(filename, encoding='utf-8') as f:
        for line in f:
            line_count += 1
    return line_count

#Проверка на уникальность
def CheckUniq(massive, line):
    for string in massive:
        if string == line:
            return False
    return True

#Генерация publisher
UniqPublisher = []


City = None
Name = None

#Случайная строка из City
count = 0
QueryFw = open("sql/publisher.sql", "w", encoding='utf-8')
while(count < 2000):
    PublisherFr = open("publisher.txt", "r", encoding='utf-8')

    CityIndex = random.randint(1,NodeCount("city.txt"))
    NameIndex = random.randint(1,NodeCount("publisher.txt"))

    City = None
    Name = None

    #Случайная строка из Name
    for i in range(0, NameIndex):
        Name = PublisherFr.readline()

    Name = Name.rstrip()
    Telephone = str(random.randint(0,1000)) + str(random.randint(0,1000)) + str(random.randint(0,1000)) + str(random.randint(0,1000))
    #Проверка на уникальность и запись
    line = str(Name +"|" + str(CityIndex)+"|" + Telephone)
    if CheckUniq(UniqPublisher, line):
        UniqPublisher.append(line)
        publisher = line.split("|") #1- Имя,2-Город,3-Телефон
        QueryFw.write("INSERT INTO publisher (publisher_name, city_id, phone, create_date) VALUES('{0}', {1}, '{2}', {3});\n".format(publisher[0], publisher[1], publisher[2], random.randint(1901,2021)))
        QueryFw.close()
        QueryFw = open("sql/publisher.sql", "a", encoding='utf-8')
        count = count + 1
    PublisherFr.close()
QueryFw.close()

#Генерация магазинов
UniqShop = []
QueryFw = open("sql/shop.sql", "w", encoding='utf-8')
count = 0



while(count < 2000):
    ShopFr = open("shop.txt", "r", encoding='utf-8')

    Shop = None
    ShopIndex = random.randint(1,NodeCount("shop.txt"))
    Area = random.randint(1,NodeCount("area.txt"))
    Own = random.randint(1, NodeCount("own.txt"))

    

    for i in range(0, ShopIndex):
        Shop = ShopFr.readline()


    Shop = Shop.rstrip()

    line = str(Shop + "|" + str(Area) + "|Дом " + str(random.randint(1,100)))
    if CheckUniq(UniqShop, line):
            UniqShop.append(line)
            shop = line.split("|") #1- Имя,2-Город,3-Телефон
            QueryFw.write("INSERT INTO shop (shop_name, id_area, address, id_own, date_open) VALUES('{0}', {1}, '{2}', {3}, {4});\n".format(shop[0], shop[1], shop[2], Own, str(random.randint(2001,2021))))
            QueryFw.close()
            QueryFw = open("sql/shop.sql", "a", encoding='utf-8')
            count = count + 1

    ShopFr.close()

QueryFw.close()


#Генерация книг
UniqBook = []
QueryFw = open("sql/book.sql", "w", encoding='utf-8')
count = 0



while(count < 2000):
    BookFr = open("book.txt", "r", encoding='utf-8')

    Book = None
    BookIndex = random.randint(1,NodeCount("book.txt"))
    PublisherIndex = random.randint(1,NodeCount("publisher.txt"))
    StyleIndex = random.randint(1,NodeCount("style.txt"))
    LangIndex = random.randint(1, NodeCount("lang.txt"))
    BindIndex = random.randint(1, NodeCount("binding.txt"))
    DescIndex = random.randint(1, NodeCount("description.txt"))
    DescFr = open("description.txt", "r", encoding='utf-8')
    Desc = None
    for i in range(0, DescIndex):
        Desc = DescFr.readline()
    DescFr.close()

    for i in range(0, BookIndex):
        Book = BookFr.readline()
    BookFr.close()

    Desc = Desc.rstrip()
    Book = Book.rstrip()

    line = str(Book + "|" + str(random.randint(0,2000)))
    if CheckUniq(UniqBook, line):
            UniqBook.append(line)
            book = line.split("|") #1- Имя,2-Город,3-Телефон
            QueryFw.write("INSERT INTO book (book_name, book_photo, book_description, book_lang_id, book_date, book_publisher_id, book_style_id, book_binding_id, book_date_public) VALUES('{0}', '{1}', '{2}', {3}, {4}, {5}, {6}, {7}, {8});\n".format(book[0], book[1], Desc, LangIndex, random.randint(2001,2021), PublisherIndex, StyleIndex, BindIndex, random.randint(1800,2020)))
            QueryFw.close()
            QueryFw = open("sql/book.sql", "a", encoding='utf-8')
            count = count + 1

    BookFr.close()
QueryFw.close()

#Генерация книги-авторы
UniqBookAuthor = []
QueryFw = open("sql/book_author.sql", "w", encoding='utf-8')
count = 0

while(count < 2000):
    BookIndex = random.randint(1, 2000)
    AuthorIndex = random.randint(1, NodeCount("author.txt"))

    line = str(str(BookIndex) + "|" + str(AuthorIndex))
    if CheckUniq(UniqBookAuthor, line):
            UniqBookAuthor.append(line)
            BookAuthor = line.split("|") #1- Имя,2-Город,3-Телефон
            QueryFw.write("INSERT INTO book_author (id_book, id_author) VALUES({0}, {1});\n".format(BookAuthor[0], BookAuthor[1]))
            QueryFw.close()
            QueryFw = open("sql/book_author.sql", "a", encoding='utf-8')
            count = count + 1

QueryFw.close()

#Генерация поставок
UniqDeliveries = []
QueryFw = open("sql/deliveries.sql", "w", encoding='utf-8')
count = 0

while(count < 2000):
    ShopIndex = random.randint(1,2000)
    BookIndex = random.randint(1,2000)
    Count = random.randint(1,99)
    Day = random.randint(1,28)
    Month = random.randint(1,12)
    Year = random.randint(2005,2021)

    #Проверка даты
    if Year > d.today().year:
        continue
    elif Year == d.today().year:
        if Month > d.today().month:
            continue
        elif Month == d.today().month:
            if Day > d.today().day:
                continue
    Date = str(Day) + "." + str(Month) + "." + str(Year)
    ShopPrice = random.randint(50,3000)
    Price = random.randint(10, 3000)
    LangIndex = random.randint(1, NodeCount("lang.txt"))
    Size = random.randint(10,1000)
    PreOrder = (random.randint(1,10) % 2 == 0)
    line = str(str(ShopIndex) + "|" + Date)

    if CheckUniq(UniqDeliveries, line):
                UniqDeliveries.append(line)
                QueryFw.write("INSERT INTO deliveries (shop_id, book_id, book_count, date_come, cost, lang_id, size, pre_order, def_cost) VALUES({0}, {1}, {2}, '{3}', {4}, {5}, {6}, {7}, {8});\n".format(ShopIndex, BookIndex, Count, Date, ShopPrice, LangIndex, Size, PreOrder, Price))
                QueryFw.close()
                QueryFw = open("sql/deliveries.sql", "a", encoding='utf-8')
                count = count + 1
            
print("Success")