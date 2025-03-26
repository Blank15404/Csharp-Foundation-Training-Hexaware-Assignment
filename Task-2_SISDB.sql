use SISDB;

--Task2 
--1)Write an SQL query to insert a new student into the "Students" table
Insert into Students (student_id, first_name, last_name, date_of_birth, email, phone_number)
Values (1011, 'John', 'Doe', '1995-08-15', 'john.doe@example.com', '1234567890');

select * from Students;

--2)Write an SQL query to enroll a student in a course.
Insert into Enrollments (enrollment_id, student_id, course_id, enrollment_date)
Values (11, 1001, 2, '2025-03-10');

select * from Enrollments;

--3)Update the email address of a specific teacher in the "Teacher" table. Choose any teacher and modify their email address.
select * from Teacher;

Update Teacher 
Set email = 'srinivasan.newemail@gmail.com' where teacher_id = 101;

--4)Write an SQL query to delete a specific enrollment record from the "Enrollments" table. Select an enrollment record based on the student and course.
Delete from Enrollments
where student_id = 1001 AND course_id = 2;

select * from Enrollments;

--5)Update the "Courses" table to assign a specific teacher to a course. Choose any course and teacher from the respective tables.
Update Courses
set teacher_id = 105 where course_id = 1;

Select * from Courses;

--6)Delete a specific student from the "Students" table and remove all their enrollment records from the "Enrollments" table. 
Delete from Students
where student_id = 1010;

--I had created two foreign key dependencies on the Student Table: So have to alter Enrollments and Payment with cascade.
Alter table Enrollments drop Constraint FK_Enrollment_StudentID;
Alter table Payment drop Constraint FK_Payment_StudentID;

Alter table Enrollments add Constraint FK_Enrollment_StudentID Foreign Key (student_id) references Students(student_id) on delete cascade;
Alter table Payments add Constraint FK_Payment_StudentID Foreign Key (student_id) references Students(student_id) on delete cascade;

--Try deleting Again
Delete from Students
where student_id = 1010;

Select * from Students;
Select * from Enrollments;
Select * from Payments;

--7)Update the payment amount for a specific payment record in the "Payments" table. Choose any payment record and modify the payment amount. 
Update Payments
set amount = 550.00 where payment_id = 5;

Select * from Payments;