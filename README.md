# acs

Project for TDD class

## Creating Database

- Install mysql
- Start mysql daemon: `$ mysqld`
- Login as root (in another console): 
```
$ mysql -u root -p
Enter password: (blank by default)
```
- Create the user:
```
mysql> GRANT ALL PRIVILEGES ON *.* TO 'user'@'localhost' IDENTIFIED BY 'password';
Query OK, 0 rows affected, 1 warning (0.01 sec)
mysql> \q
Bye
```
- Login as the user and create the database:
```
$ mysql -u user -p
Enter password: password
mysql> CREATE DATABASE partytalk;
Query OK, 1 row affected (0.01 sec)
```
