--Task1-Database Design:
--Create Database
Create Database SISDB;

--Define Schema for the Students, Courses, Enrollments, Teacher, and Payments tables
--STUDENTS TABLE
Create Table Students ( student_id int constraint PK_Student_ID Primary Key,
first_name varchar (30),
last_name varchar (30),
date_of_birth date,
email varchar (100),
phone_number varchar (20) );

--TEACHER TABLE
Create Table Teacher ( teacher_id int constraint PK_Teacher_ID Primary Key,
first_name varchar (30),
last_name varchar (30),
email varchar (100) );

--COURSES TABLE
Create Table Courses ( course_id int constraint PK_Course_ID Primary Key,
course_name varchar (100),
credits int,
teacher_id int constraint FK_Course_TeacherID Foreign Key references Teacher (teacher_id) );

--ENROLLMENTS TABLE
Create Table Enrollments ( enrollment_id int constraint PK_Enrollment_ID Primary Key,
student_id int constraint FK_Enrollment_StudentID Foreign Key references Students (student_id),
course_id int constraint FK_Enrollment_CourseID Foreign Key references Courses (course_id),
enrollment_date date );

--PAYMENTS TABLE
Create Table Payments ( payment_id int constraint PK_Payment_ID Primary Key,
student_id int constraint FK_Payment_StudentID Foreign Key references Students (student_id),
amount decimal (10,2),
payment_date date );

--Insert at least 10 sample records into each of the following tables
--STUDENTS
Insert into Students ( student_id, first_name, last_name, date_of_birth, email, phone_number ) 
values (1001, 'Karthik', 'Raj', '2000-03-12', 'karthik.raj12@gmail.com', '9876543210'),
(1002, 'Priya', 'Menon', '1999-07-25', 'priya_menon99@gmail.com', '8765432109'),
(1003, 'Arjun', 'Nair', '2001-11-08', 'arjun.nair01@gmail.com', '7654321098'),
(1004, 'Divya', 'Pillai', '2000-05-19', 'divya.pillai@gmail.com', '6543210987'),
(1005, 'Suresh', 'Iyengar', '1999-09-30', 'suresh.i@gmail.com', '5432109876'),
(1006, 'Ananya', 'Reddy', '2001-02-14', 'ananya.reddy@gmail.com', '4321098765'),
(1007, 'Vijay', 'Kumar', '2000-08-22', 'vijaykumar88@gmail.com', '3210987654'),
(1008, 'Lakshmi', 'Venkatesh', '1999-12-05', 'lakshmi.venkatesh@gmail.com', '2109876543'),
(1009, 'Ramesh', 'Gowda', '2001-04-18', 'ramesh.gowda@gmail.com', '1098765432'),
(1010, 'Swathi', 'Rao', '2000-06-21', 'swathi_rao@gmail.com', '0987654321');

Select * from Students ;


--TEACHER
Insert into Teacher ( teacher_id, first_name, last_name, email )
values (101, 'Srinivasan', 'Iyengar', 'srinivasan.i@gmail.com'),
(102, 'Geetha', 'Krishnan', 'geetha_krishnan@gmail.com'),
(103, 'Rajesh', 'Pillai', 'rajesh.pillai@gmail.com'),
(104, 'Latha', 'Menon', 'latha.menon@gmail.com'),
(105, 'Murali', 'Nair', 'murali.nair@gmail.com'),
(106, 'Anjali', 'Reddy', 'anjali.reddy@gmail.com'),
(107, 'Venkat', 'Subramanian', 'venkat.subramanian@gmail.com'),
(108, 'Shobha', 'Gowda', 'shobha.gowda@gmail.com'),
(109, 'Krishna', 'Kumar', 'krishna.kumar@gmail.com'),
(110, 'Meera', 'Rao', 'meera.rao@gmail.com');

Select * from Teacher ;


--COURSES
Insert into Courses ( course_id, course_name, credits, teacher_id )
values (1, 'Mathematics', 4, 101),
(2, 'Physics', 3, 102),
(3, 'Chemistry', 3, 103),
(4, 'Biology', 3, 104),
(5, 'History', 2, 105),
(6, 'Geography', 2, 106),
(7, 'English', 3, 107),
(8, 'Computer Science', 4, 108),
(9, 'Art', 2, 109),
(10, 'Music', 2, 110);

Select * from Courses ;


--ENROLLMENTS
Insert into Enrollments ( enrollment_id, student_id, course_id, enrollment_date )
values (1, 1001, 1, '2025-01-05'),
(2, 1002, 2, '2025-01-10'),
(3, 1003, 3, '2025-01-15'),
(4, 1004, 4, '2025-01-20'),
(5, 1005, 5, '2025-01-25'),
(6, 1006, 6, '2025-02-01'),
(7, 1007, 7, '2025-02-05'),
(8, 1008, 8, '2025-02-10'),
(9, 1009, 9, '2025-02-15'),
(10, 1010, 10, '2025-02-20');

Select * from Enrollments ;


--PAYMENTS
Insert into Payments ( payment_id, student_id, amount, payment_date )
values (1, 1001, 500.00, '2025-01-15'),
(2, 1002, 600.00, '2025-01-20'),
(3, 1003, 550.00, '2025-01-25'),
(4, 1004, 700.00, '2025-02-01'),
(5, 1005, 450.00, '2025-02-05'),
(6, 1006, 800.00, '2025-02-10'),
(7, 1007, 500.00, '2025-02-15'),
(8, 1008, 650.00, '2025-02-20'),
(9, 1009, 750.00, '2025-02-25'),
(10, 1010, 900.00, '2025-02-28');

Select * from Payments ;