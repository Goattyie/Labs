"INSERT INTO book (book_name, book_photo, book_description, book_lang_id, book_date, book_publisher_id, book_style_id, book_binding_id, book_date_public) VALUES ('Библия Мациевского', '0', 'Советский шофёр жил с любимой женой и растил детей. Началась война. Он ушёл на фронт, попал в плен, но героически оттуда сбежал. Узнав, что вся его семья погибла, он усыновил мальчика-беспризорника.', (SELECT id_lang FROM lang WHERE name_lang = 'Словенский язык'), 2006, (SELECT publisher_id FROM publisher WHERE publisher_name = 'Де Агостини' LIMIT 1),(SELECT id_style FROM style WHERE name_style = 'поэма'),(SELECT id_binding FROM binding WHERE name_binding = 'Твердый'), 1901)"
"INSERT INTO book (book_name, book_photo, book_description, book_lang_id, book_date, book_publisher_id, book_style_id, book_binding_id, book_date_public) VALUES ('Бесконечная история (повесть)', '0', 'Мелкий чиновник полжизни копит на маленькое поместье, голодает. Наконец, его мечта исполняется, и чиновник превращается в толстого, самодовольного барина, самоуверенно рассуждающего о нуждах народа.', (SELECT id_lang FROM lang WHERE name_lang = 'Японский язык'), 2020, (SELECT publisher_id FROM publisher WHERE publisher_name = 'Знание (издательство, Санкт-Петербург)' LIMIT 1),(SELECT id_style FROM style WHERE name_style = 'роман-эпопея'),(SELECT id_binding FROM binding WHERE name_binding = 'Твердый'), 2015)"
"INSERT INTO book (book_name, book_photo, book_description, book_lang_id, book_date, book_publisher_id, book_style_id, book_binding_id, book_date_public) VALUES ('Библия, Коран и наука', '0', 'Рассказчик смотрит на школьную фотографию и вспоминает о друге детства, бабушке, родной избе, раскула­чивании, деревенском быте и семье молодых учителей, которые организовали школу в его глухом селе.', (SELECT id_lang FROM lang WHERE name_lang = 'Датский язык'), 2005, (SELECT publisher_id FROM publisher WHERE publisher_name = 'Аванта+' LIMIT 1),(SELECT id_style FROM style WHERE name_style = 'трагедия'),(SELECT id_binding FROM binding WHERE name_binding = 'Мягкий'), 2006)"
"INSERT INTO book (book_name, book_photo, book_description, book_lang_id, book_date, book_publisher_id, book_style_id, book_binding_id, book_date_public) VALUES ('Алла и Рождество', '0', 'Садко — молодой гусляр из Великого Новгорода. В начале рассказа он беден, горд и самолюбив. Его единственное достояние — яровчатые гусли, на которых он играет, переходя с одного весёлого застолья к другому.', (SELECT id_lang FROM lang WHERE name_lang = 'Английский'), 2012, (SELECT publisher_id FROM publisher WHERE publisher_name = 'Манн, Иванов и Фербер' LIMIT 1),(SELECT id_style FROM style WHERE name_style = 'поэма'),(SELECT id_binding FROM binding WHERE name_binding = 'Твердый'), 1808)"
"INSERT INTO book (book_name, book_photo, book_description, book_lang_id, book_date, book_publisher_id, book_style_id, book_binding_id, book_date_public) VALUES ('Анжелика (серия книг)', '0', 'Деревенский мальчик спасает тонущего в омуте приятеля и плачет от радости и пережитого страха.', (SELECT id_lang FROM lang WHERE name_lang = 'Монгольский язык'), 2012, (SELECT publisher_id FROM publisher WHERE publisher_name = 'wqeqwe' LIMIT 1),(SELECT id_style FROM style WHERE name_style = 'миф'),(SELECT id_binding FROM binding WHERE name_binding = 'Мягкий'), 1810)"
"INSERT INTO book (book_name, book_photo, book_description, book_lang_id, book_date, book_publisher_id, book_style_id, book_binding_id, book_date_public) VALUES ('Африканец в Гренландии', '0', 'Деревенский мальчик спасает тонущего в омуте приятеля и плачет от радости и пережитого страха.', (SELECT id_lang FROM lang WHERE name_lang = 'Молдавский язык'), 2016, (SELECT publisher_id FROM publisher WHERE publisher_name = '«Юрайт» (для авторов учебников)' LIMIT 1),(SELECT id_style FROM style WHERE name_style = 'аполог'),(SELECT id_binding FROM binding WHERE name_binding = 'Твердый'), 1947)"
"INSERT INTO book (book_name, book_photo, book_description, book_lang_id, book_date, book_publisher_id, book_style_id, book_binding_id, book_date_public) VALUES ('Анналы династии Чосон', '0', 'Женщина всю жизнь работала в колхозе. Детей и мужа она потеряла, но сохранила доброту и бескорыстно всем помогала. После её нелепой и страшной смерти родные делят её дом и вспоминают о ней с укором.', (SELECT id_lang FROM lang WHERE name_lang = 'Шведский язык'), 2003, (SELECT publisher_id FROM publisher WHERE publisher_name = 'Матезис' LIMIT 1),(SELECT id_style FROM style WHERE name_style = 'аполог'),(SELECT id_binding FROM binding WHERE name_binding = 'Мягкий'), 1949)"
"INSERT INTO book (book_name, book_photo, book_description, book_lang_id, book_date, book_publisher_id, book_style_id, book_binding_id, book_date_public) VALUES ('Английские розы. Любовь и дружба', '0', 'Известный учёный превратил пса в человека. Тот оказался редким мерзавцем: хамил, напивался, приставал к женщинам, требовал жилплощадь и писал доносы. Учёный не выдержал и превратил его обратно в пса.', (SELECT id_lang FROM lang WHERE name_lang = 'Таджикский язык'), 2014, (SELECT publisher_id FROM publisher WHERE publisher_name = 'Медиарама' LIMIT 1),(SELECT id_style FROM style WHERE name_style = 'роман-эпопея'),(SELECT id_binding FROM binding WHERE name_binding = 'Мягкий'), 1814)"
"INSERT INTO book (book_name, book_photo, book_description, book_lang_id, book_date, book_publisher_id, book_style_id, book_binding_id, book_date_public) VALUES ('Библия, Коран и наука', '0', 'Деревенский мальчик спасает тонущего в омуте приятеля и плачет от радости и пережитого страха.', (SELECT id_lang FROM lang WHERE name_lang = 'Фламандский язык'), 2020, (SELECT publisher_id FROM publisher WHERE publisher_name = 'Де Агостини' LIMIT 1),(SELECT id_style FROM style WHERE name_style = 'повесть'),(SELECT id_binding FROM binding WHERE name_binding = 'Твердый'), 1933)"
"INSERT INTO book (book_name, book_photo, book_description, book_lang_id, book_date, book_publisher_id, book_style_id, book_binding_id, book_date_public) VALUES ('Аглая (альманах)', '0', 'У старика сжималось сердце при виде выброшенных сломанных кукол без рук и ног: за годы войны он «нагляделся человечины». Найдя куклу, над которой жестоко надругались, он хоронит её как человека.', (SELECT id_lang FROM lang WHERE name_lang = 'Дари (Фарси)'), 2020, (SELECT publisher_id FROM publisher WHERE publisher_name = 'Издательство В. И. Губинского' LIMIT 1),(SELECT id_style FROM style WHERE name_style = 'аполог'),(SELECT id_binding FROM binding WHERE name_binding = 'Мягкий'), 1951)"
"INSERT INTO book (book_name, book_photo, book_description, book_lang_id, book_date, book_publisher_id, book_style_id, book_binding_id, book_date_public) VALUES ('Банальность зла: Эйхман в Иерусалиме', '0', 'Деревенский мальчик спасает тонущего в омуте приятеля и плачет от радости и пережитого страха.', (SELECT id_lang FROM lang WHERE name_lang = 'Шведский язык'), 2002, (SELECT publisher_id FROM publisher WHERE publisher_name = 'Де Агостини' LIMIT 1),(SELECT id_style FROM style WHERE name_style = 'баллада'),(SELECT id_binding FROM binding WHERE name_binding = 'Твердый'), 1944)"
"INSERT INTO book (book_name, book_photo, book_description, book_lang_id, book_date, book_publisher_id, book_style_id, book_binding_id, book_date_public) VALUES ('Банальность зла: Эйхман в Иерусалиме', '0', 'Мелкий чиновник полжизни копит на маленькое поместье, голодает. Наконец, его мечта исполняется, и чиновник превращается в толстого, самодовольного барина, самоуверенно рассуждающего о нуждах народа.', (SELECT id_lang FROM lang WHERE name_lang = 'Тайский язык'), 2011, (SELECT publisher_id FROM publisher WHERE publisher_name = 'Истари комикс' LIMIT 1),(SELECT id_style FROM style WHERE name_style = 'трагедия'),(SELECT id_binding FROM binding WHERE name_binding = 'Мягкий'), 1970)"
"INSERT INTO book (book_name, book_photo, book_description, book_lang_id, book_date, book_publisher_id, book_style_id, book_binding_id, book_date_public) VALUES ('Береника (Расин)', '0', 'Учитель гимназии, боящийся всего на свете и живущий согласно распоряжениям начальства, решает жениться. Долгое сватовство извлекает учителя из его «футляра», и он умирает, испугавшись реальной жизни.', (SELECT id_lang FROM lang WHERE name_lang = 'Таджикский язык'), 2008, (SELECT publisher_id FROM publisher WHERE publisher_name = '«РИПОЛ Классик»' LIMIT 1),(SELECT id_style FROM style WHERE name_style = 'элегия'),(SELECT id_binding FROM binding WHERE name_binding = 'Твердый'), 1801)"
"INSERT INTO book (book_name, book_photo, book_description, book_lang_id, book_date, book_publisher_id, book_style_id, book_binding_id, book_date_public) VALUES ('Библиотекарь (роман)', '0', 'Женщина всю жизнь работала в колхозе. Детей и мужа она потеряла, но сохранила доброту и бескорыстно всем помогала. После её нелепой и страшной смерти родные делят её дом и вспоминают о ней с укором.', (SELECT id_lang FROM lang WHERE name_lang = 'Фарси (Дари)'), 2020, (SELECT publisher_id FROM publisher WHERE publisher_name = 'Международные отношения (издательство)' LIMIT 1),(SELECT id_style FROM style WHERE name_style = 'элегия'),(SELECT id_binding FROM binding WHERE name_binding = 'Мягкий'), 1870)"
"INSERT INTO book (book_name, book_photo, book_description, book_lang_id, book_date, book_publisher_id, book_style_id, book_binding_id, book_date_public) VALUES ('Библия', '0', 'Деревенский мальчик спасает тонущего в омуте приятеля и плачет от радости и пережитого страха.', (SELECT id_lang FROM lang WHERE name_lang = 'Корейский язык'), 2015, (SELECT publisher_id FROM publisher WHERE publisher_name = 'Ир (издательство)' LIMIT 1),(SELECT id_style FROM style WHERE name_style = 'поэма'),(SELECT id_binding FROM binding WHERE name_binding = 'Мягкий'), 1902)"
"INSERT INTO book (book_name, book_photo, book_description, book_lang_id, book_date, book_publisher_id, book_style_id, book_binding_id, book_date_public) VALUES ('Арадия, или Евангелие ведьм', '0', 'Советский шофёр жил с любимой женой и растил детей. Началась война. Он ушёл на фронт, попал в плен, но героически оттуда сбежал. Узнав, что вся его семья погибла, он усыновил мальчика-беспризорника.', (SELECT id_lang FROM lang WHERE name_lang = 'Малазийский язык'), 2006, (SELECT publisher_id FROM publisher WHERE publisher_name = 'Ир (издательство)' LIMIT 1),(SELECT id_style FROM style WHERE name_style = 'эпос'),(SELECT id_binding FROM binding WHERE name_binding = 'Мягкий'), 1852)"
"INSERT INTO book (book_name, book_photo, book_description, book_lang_id, book_date, book_publisher_id, book_style_id, book_binding_id, book_date_public) VALUES ('Алюмен', '0', 'Мелкий чиновник полжизни копит на маленькое поместье, голодает. Наконец, его мечта исполняется, и чиновник превращается в толстого, самодовольного барина, самоуверенно рассуждающего о нуждах народа.', (SELECT id_lang FROM lang WHERE name_lang = 'Литовский язык'), 2014, (SELECT publisher_id FROM publisher WHERE publisher_name = 'Де Агостини' LIMIT 1),(SELECT id_style FROM style WHERE name_style = 'элегия'),(SELECT id_binding FROM binding WHERE name_binding = 'Твердый'), 1940)"
"INSERT INTO book (book_name, book_photo, book_description, book_lang_id, book_date, book_publisher_id, book_style_id, book_binding_id, book_date_public) VALUES ('Американский психопат', '0', 'Имение разорённой вдовы выставлено на торги. Купец советует ей вырубить сад и сдать землю в аренду. Она против — в этом саду прошла её молодость. Тогда он выкупает имение и сам реализует свой план.', (SELECT id_lang FROM lang WHERE name_lang = 'Словенский язык'), 2012, (SELECT publisher_id FROM publisher WHERE publisher_name = 'Де Агостини' LIMIT 1),(SELECT id_style FROM style WHERE name_style = 'трагедия'),(SELECT id_binding FROM binding WHERE name_binding = 'Мягкий'), 1948)"
"INSERT INTO book (book_name, book_photo, book_description, book_lang_id, book_date, book_publisher_id, book_style_id, book_binding_id, book_date_public) VALUES ('Абарат', '0', 'Надвинулась великая беда. Чудовищный многоголовый Змей стал совершать на Русь налёты, похищая как неповинных мирных людей, так и славных воинов, которые не могли воспротивиться ему. Добрыня Никитич решил сразиться со Змеем. Он не послушался матери, которая остерегала его от того, чтобы ехать далеко в чисто поле, на Сорочинскую гору, где ползают малые змеёныши. Ещё не велела она купаться в Пучай-реке.', (SELECT id_lang FROM lang WHERE name_lang = 'Японский язык'), 2001, (SELECT publisher_id FROM publisher WHERE publisher_name = 'Мир хобби' LIMIT 1),(SELECT id_style FROM style WHERE name_style = 'рассказ'),(SELECT id_binding FROM binding WHERE name_binding = 'Мягкий'), 1805)"
"INSERT INTO book (book_name, book_photo, book_description, book_lang_id, book_date, book_publisher_id, book_style_id, book_binding_id, book_date_public) VALUES ('Библия', '0', 'Учитель гимназии, боящийся всего на свете и живущий согласно распоряжениям начальства, решает жениться. Долгое сватовство извлекает учителя из его «футляра», и он умирает, испугавшись реальной жизни.', (SELECT id_lang FROM lang WHERE name_lang = 'Турецкий язык'), 2014, (SELECT publisher_id FROM publisher WHERE publisher_name = 'Издательство Петроградского совета' LIMIT 1),(SELECT id_style FROM style WHERE name_style = 'скетч'),(SELECT id_binding FROM binding WHERE name_binding = 'Твердый'), 1840)"
