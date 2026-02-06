CREATE TABLE Department(
Id bigint Primary Key IDENTITY(1,1) NOT NULL,
Name varchar(15) NOT NULL,
UNIQUE (Name),
);

CREATE TABLE Employee(
Id bigint Primary Key IDENTITY(1,1) NOT NULL,
Username nvarchar(15) NOT NULL,
Password nvarchar(255) NOT NULL,
Email nvarchar(255) NOT NULL,
RoleId int NOT NULL,
Phone_Number decimal(8, 0) NOT NULL,
Birthdate date NOT NULL,
Living_Address varchar(30) NOT NULL,
Gender char(1) NOT NULL,
Salary int NOT NULL,
MaritalStatusId int NOT NULL,
DepartmentId bigint,
UNIQUE (Username),
UNIQUE (Email),
FOREIGN KEY(DepartmentId)
REFERENCES Department(Id)
ON UPDATE CASCADE
ON DELETE SET NULL
);

CREATE TABLE Task(
Id bigint Primary Key IDENTITY(1,1) NOT NULL,
Name varchar(15) NOT NULL,
DepartmentId bigint,
CreationDate date NOT NULL,
ClosureDate date NOT NULL,
StatusId int NOT NULL,
UNIQUE (Name),
FOREIGN KEY(DepartmentId)
REFERENCES Department(Id)
ON UPDATE CASCADE
ON DELETE SET NULL
);

CREATE TABLE TaskAssignment(
Id bigint Primary Key Identity(1,1) NOT NULL,
TaskId bigint NULL,
EmployeeId bigint,
FOREIGN KEY(EmployeeId)
REFERENCES Employee(Id)
ON UPDATE CASCADE
ON DELETE SET NULL,
FOREIGN KEY(TaskId)
REFERENCES Task(Id)
);

CREATE TABLE TimeSlice(
Id bigint Primary Key Identity(1,1) NOT NULL,
TaskAssignmentId bigint,
StartDate date NOT NULL,
EndDate date NOT NULL,
FOREIGN KEY(TaskAssignmentId)
REFERENCES TaskAssignment(Id)
ON UPDATE CASCADE
ON DELETE SET NULL
);