use SISDB;

--Task3 Aggregate functions, Having, Order By, GroupBy and Joins: 
--1)Write an SQL query to calculate the total payments made by a specific student. 
--adding extra datas to show total
Insert into Enrollments (enrollment_id, student_id, course_id, enrollment_date) 
values (14, 1001, 3, '2025-03-01'), 
(15, 1001, 7, '2025-03-10');

Insert into Payments (payment_id, student_id, amount, payment_date)
values (11, 1001, 300.00, '2025-03-05'),  
(12, 1001, 250.00, '2025-03-15');

--Total Payment made by student ID 1001
Select s.first_name, s.last_name, SUM(p.amount) AS total_payments from Students s
join Payments p on s.student_id = p.student_id
where s.student_id = 1001
group by s.first_name, s.last_name;

--2)Write an SQL query to retrieve a list of courses along with the count of students enrolled in each course.
Select c.course_name, count(e.student_id) as student_count from Courses c
left join Enrollments e on c.course_id = e.course_id
group by c.course_name;

--3)Write an SQL query to find the names of students who have not enrolled in any course. 
Select s.first_name, s.last_name from Students s
left join Enrollments e on s.student_id = e.student_id
where e.enrollment_id is null;

--4)Write an SQL query to retrieve the first name, last name of students, and the names of the courses they are enrolled in.
Select s.first_name, s.last_name, c.course_name from Students s
join Enrollments e on s.student_id = e.student_id
join Courses c on e.course_id = c.course_id;

--5)Create a query to list the names of teachers and the courses they are assigned to.
Select (t.first_name +' '+ t.last_name) as teacher_name, c.course_name from Teacher t
join Courses c on t.teacher_id = c.teacher_id;

--6)Retrieve a list of students and their enrollment dates for a specific course.
Select s.first_name, s.last_name, e.enrollment_date from Students s
join Enrollments e on s.student_id=e.student_id
join Courses c on e.course_id=c.course_id where c.course_id=3;

--7)Find the names of students who have not made any payments.
Insert into Students (student_id, first_name, last_name, date_of_birth, email, phone_number) 
values (1012, 'Noah', 'Smith', '2001-05-15', 'noah.smith@example.com', '5551234567'),
(1013, 'Manish', 'Sivakumar', '1999-09-09', 'noah.smith@example.com', '5551234567'),
(1014, 'Ismail', 'Ali', '2000-03-25', 'noah.smith@example.com', '5551234567');

Insert into Enrollments (enrollment_id, student_id, course_id, enrollment_date)
values (11, 1012, 1, '2023-09-01'),
(12, 1013, 3, '2023-09-01'),
(13, 1014, 5, '2023-09-01');

Select (s.first_name+' '+s.last_name) as student_name from Students s
join Enrollments e on s.student_id=e.student_id
left join Payments p on e.student_id=p.student_id where p.payment_date is null;

--8)Write a query to identify courses that have no enrollments.
Insert into Courses (course_id, course_name, credits, teacher_id)
values (11, 'Advanced Digital Painting', 3, 103),
(12, 'Modern Sculpture', 3, 106);

select c.course_id, c.course_name from courses c 
left join enrollments e on c.course_id = e.course_id where e.course_id is null;

select * from Enrollments;
select * from Students;
select * from Courses;

--9)Identify students who are enrolled in more than one course.
Insert into Enrollments (enrollment_id, student_id, course_id, enrollment_date) 
values (16, 1001, 2, '2023-09-05'),
(17, 1002, 3, '2023-09-10'),
(18, 1002, 4, '2023-09-15'),
(19, 1003, 5, '2023-09-20');

Select (s.first_name+' '+s.last_name) as student_name, count(e.course_id) as course_count from Students s
join Enrollments e on s.student_id=e.student_id 
group by s.first_name,s.last_name having count(e.course_id)>1;

--10)Find teachers who are not assigned to any courses.
INSERT INTO Teacher (teacher_id, first_name, last_name, email) VALUES
(111, 'Sam', 'Karan', 'sam.karan@example.com'),
(112, 'Vimal', 'Raj', 'vimal@example.com'),
(113, 'Ali', 'Baskar', 'ali_baskar@example.com');

Select (t.first_name+' '+t.last_name) as teacher_name from Teacher t
left join Courses c on t.teacher_id=c.teacher_id where c.teacher_id is null;

Select* from Teacher;