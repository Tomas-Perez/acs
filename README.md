# acs

Project for TDD class

## Creating Database

### Locally

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

### Docker

- Run the container:
```
$ docker run --name NAME -e MYSQL_ROOT_PASSWORD=root -p 3306:3306 -d mysql
```
- Execute:
```
$ docker exec -it CONTAINER /bin/bash
```
- Login as root:
```
$ mysql -u root -p
Enter password: root
```
- Create the user:
```
mysql> CREATE USER 'user'@'%' IDENTIFIED BY 'password';
mysql> GRANT ALL PRIVILEGES ON *.* TO 'user'@'%' WITH GRANT OPTION;
```
- Create the database:
```
mysql> CREATE DATABASE partytalk;
```

## Running in Docker

- Pull the image:
```
docker pull dwape/partytalk-light
```
- Run the container:
```
docker run -i -p 1234:1234 -e DB_SERVER=[Database_IP] dwape/partytalk-light
```
