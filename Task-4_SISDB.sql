use SISDB;

--Task 4. Subquery and its type:
--1)Write an SQL query to calculate the average number of students enrolled in each course.
Select avg(student_count) as avg_students_per_course
from ( select course_id, count(student_id) as student_count from Enrollments
group by course_id) as course_counts;

--2)Identify the student(s) who made the highest payment.
Select s.first_name, s.last_name, p.amount from Students s
join Payments p on s.student_id = p.student_id
where p.amount = (Select max(amount) from Payments);

--3)Retrieve a list of courses with the highest number of enrollments.
select c.course_name, count(e.student_id) as enrollment_count from Courses c
join Enrollments e on c.course_id=e.course_id
group by c.course_name having count(e.student_id) = (
select max(enrollment_count) from (
select count(student_id) as enrollment_count from enrollments group by course_id) as max_counts);

select * from Courses;

--4)Calculate the total payments made to courses taught by each teacher.
select t.teacher_id, t.first_name, t.last_name, (
select sum(p.amount) from payments p where p.student_id in (
select e.student_id from enrollments e 
join courses c on e.course_id = c.course_id where c.teacher_id = t.teacher_id)) as total_payments from teacher t;

--5)Identify students who are enrolled in all available courses.
Insert into Enrollments (enrollment_id, student_id, course_id, enrollment_date)
values (20, 1012, 1, '2025-01-10'),
(21, 1012, 2, '2025-01-15'),
(22, 1012, 3, '2025-01-22'),
(23, 1012, 4, '2025-02-05'),
(24, 1012, 5, '2025-02-12'),
(25, 1012, 6, '2025-02-18'),
(26, 1012, 7, '2025-03-03'),
(27, 1012, 8, '2025-03-10'),
(28, 1012, 9, '2025-03-17'),
(29, 1012, 10, '2025-03-24'),
(30, 1012, 11, '2025-04-02'),
(31, 1012, 12, '2025-04-09'),
(32, 1013, 1, '2025-01-12'),
(33, 1013, 2, '2025-01-18'),
(34, 1013, 3, '2025-01-25'),
(35, 1013, 4, '2025-02-08'),
(36, 1013, 5, '2025-02-15'),
(37, 1013, 6, '2025-02-22'),
(38, 1013, 7, '2025-03-07'),
(39, 1013, 8, '2025-03-14'),
(40, 1013, 9, '2025-03-21'),
(41, 1013, 10, '2025-03-28'),
(42, 1013, 11, '2025-04-05'),
(43, 1013, 12, '2025-04-12');

select (s.first_name+' '+s.last_name) as StudentName from Students s
where not exists ( select c.course_id from Courses c
where not exists ( select 1 from Enrollments e where e.student_id = s.student_id and e.course_id = c.course_id));

select*from Enrollments;

--6)Retrieve the names of teachers who have not been assigned to any courses.
Select t.first_name, t.last_name from Teacher t
where t.teacher_id not in ( select distinct c.teacher_id from  Courses c where c.teacher_id is not null);

Select * from Students;

--7)Calculate the average age of all students.
Select avg(datediff(year, date_of_birth, getdate())) as average_age from Students;

--8)Identify courses with no enrollments.
Insert into Courses (course_id, course_name, credits, teacher_id)
values (13, 'Cloud Computing', 3, 103),
(14, 'Machine Learning', 4, 106);

select course_id, course_name from courses 
where course_id not in (select distinct course_id from enrollments);

select * from Courses

--9)Calculate the total payments made by each student for each course they are enrolled in.
Select s.first_name, s.last_name, c.course_name, (select sum(amount) from Payments p
where p.student_id = e.student_id ) as total_payments from Students s
join Enrollments e on s.student_id = e.student_id
join Courses c on e.course_id = c.course_id;


--10)Identify students who have made more than one payment. 
Select s.first_name, s.last_name from Students s
where (Select count(*) from Payments p where p.student_id = s.student_id) > 1;

--11)Write an SQL query to calculate the total payments made by each student. 
select s.student_id, s.first_name, s.last_name, sum(p.amount) as total_payments from students s 
join payments p on s.student_id = p.student_id 
group by s.student_id, s.first_name, s.last_name;

--12)Retrieve a list of course names along with the count of students enrolled in each course. 
select c.course_name, count(e.student_id) as student_count from courses c 
left join enrollments e on c.course_id = e.course_id 
group by c.course_name;

--13)Calculate the average payment amount made by students.
select (s.first_name+' '+s.last_name) as student_name, avg(p.amount) as avg_payment from students s 
join payments p on s.student_id = p.student_id 
group by s.student_id, s.first_name, s.last_name;
