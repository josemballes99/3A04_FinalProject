import sqlite3

connection = sqlite3.connect("Objectives.db")
cursor = connection.cursor()

#cursor.execute("CREATE TABLE IF NOT EXISTS Objectives (id INTEGER, caption VARCHAR(30), progress INTEGER, reward INTEGER, query VARCHAR(150), condition INTEGER)")

#cursor.execute("INSERT INTO Objectives (id, caption, progress, reward, query, condition) VALUES (1, 'Spend $200 on the hotel', 0, 50, 'SELECT SUM(amt) FROM COST', 200)")
#cursor.execute("INSERT INTO Objectives (id, caption, progress, reward, query, condition) VALUES (2, 'Spend $2000 on the hotel', 0, 50, 'SELECT SUM(amt) FROM COST', 2000)")
#cursor.execute("INSERT INTO Objectives (id, caption, progress, reward, query, condition) VALUES (3, 'Spend $20000 on the hotel', 0, 50, 'SELECT SUM(amt) FROM COST', 20000)")

#cursor.execute("INSERT INTO Objectives (id, caption, progress, reward, query, condition) VALUES (4, 'Accumulate $1000', 0, 50, 'SELECT SUM(amt) FROM INCOME', 1000)")
#cursor.execute("INSERT INTO Objectives (id, caption, progress, reward, query, condition) VALUES (5, 'Accumulate $2000', 0, 50, 'SELECT SUM(amt) FROM INCOME', 2000)")
#cursor.execute("INSERT INTO Objectives (id, caption, progress, reward, query, condition) VALUES (6, 'Accumulate $10000', 0, 50, 'SELECT SUM(amt) FROM INCOME', 10000)")

cursor.execute("INSERT INTO Objectives (id, caption, progress, reward, query, condition) VALUES (7, 'Build 1 new floor', 0, 50, 'SELECT COUNT(*) FROM Floors', 1)")
cursor.execute("INSERT INTO Objectives (id, caption, progress, reward, query, condition) VALUES (8, 'Build 3 new floors', 0, 100, 'SELECT COUNT(*) FROM Floors', 3)")
cursor.execute("INSERT INTO Objectives (id, caption, progress, reward, query, condition) VALUES (9, 'Build 10 new floors', 0, 150, 'SELECT COUNT(*) FROM Floors', 10)")

cursor.execute("INSERT INTO Objectives (id, caption, progress, reward, query, condition) VALUES (10, 'Upgrade a floor to level 2', 0, 50, 'SELECT COUNT(*) FROM Floors WHERE level > 1', 1)")
cursor.execute("INSERT INTO Objectives (id, caption, progress, reward, query, condition) VALUES (11, 'Upgrade a floor to level 3', 0, 50, 'SELECT COUNT(*) FROM Floors WHERE level > 2', 1)")
cursor.execute("INSERT INTO Objectives (id, caption, progress, reward, query, condition) VALUES (12, 'Upgrade 10 floors to level 3', 0, 150, 'SELECT COUNT(*) FROM Floors WHERE level > 2', 10)")

cursor.execute("INSERT INTO Objectives (id, caption, progress, reward, query, condition) VALUES (13, 'Assign 1 person to a floor', 0, 50, 'SELECT MAX(num) FROM Occupants', 1)")
cursor.execute("INSERT INTO Objectives (id, caption, progress, reward, query, condition) VALUES (14, 'Assign 10 people to floors', 0, 150, 'SELECT MAX(num) FROM Occupants', 5)")
cursor.execute("INSERT INTO Objectives (id, caption, progress, reward, query, condition) VALUES (15, 'Assign 100 people to floors', 0, 250, 'SELECT MAX(num) FROM Occupants', 10)")



##cursor.execute("CREATE TABLE Cost(tid INTEGER, source INTEGER, amt INTEGER)")
##cursor.execute("CREATE TABLE Floors(pos INTEGER, type INTEGER, level INTEGER)")
##cursor.execute("CREATE TABLE Occupants(pos INTEGER, num INTEGER)")
##cursor.execute("CREATE TABLE Upgrade(ftype INTEGER, amt INTEGER)")
