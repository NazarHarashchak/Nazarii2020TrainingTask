
CREATE DATABASE TaskManagmenting 

DROP DATABASE TaskManagmenting

USE TaskManagmenting

CREATE TABLE TaskStatuses (
	Id INT IDENTITY NOT NULL,
	TaskStatus VARCHAR(20),
	PRIMARY KEY(Id)
)
CREATE TABLE USERS (
	Id INT IDENTITY NOT NULL,
	UserFullName varchar(50) NULL,
	PRIMARY KEY(Id)
)
CREATE TABLE Tasks (
	Id INT IDENTITY NOT NULL,
	Title varchar(100) NULL,
	TaskDescription text NULL,
	CreatedUserId int,
	AssigneeUserId int,
	DueDate DATE NOT NULL,
	StatusId int,
	PRIMARY KEY(Id),
	Foreign KEY (CreatedUserId) REFERENCES USERS (Id),
	Foreign KEY (AssigneeUserId) REFERENCES USERS (Id),
	Foreign KEY (StatusId) REFERENCES TaskStatuses (Id)
)

INSERT INTO TaskStatuses
VALUES ('In Progress'),('Completed'), ('Overdue')

INSERT INTO USERS
VALUES ('Vasyl'),('Nazar'),('Dmytro'),('Ivan'),('Petro'),('Yurii')
