Create database CareerHub;

Create table Companies (CompanyID int constraint PK_Company_ID primary key,
CompanyName varchar(100) not null,
ComLocation varchar(100)not null);

Create table Jobs (JobID int constraint PK_Job_ID primary key,
CompanyID int constraint FK_Company_ID references Companies(CompanyID),
JobTitle varchar(100) not null,
JobDescription text not null,
JobLocation varchar(100) not null,
Salary decimal(10,2) not null,
JobType varchar(50) not null,
PostedDate datetime not null default GETDATE(),
Deadline datetime);

Create table Applicants (ApplicantID int constraint PK_Applicant_ID primary key,
FirstName varchar(50) not null,
LastName varchar(50) not null,
Email varchar(100) not null,
Phone varchar(20),
AppResume text);

Create table Applications (ApplicationID int constraint PK_Application_ID primary key,
JobID int constraint FK_Job_ID references Jobs(JobID) not null,
ApplicantID int constraint FK_Applicant_ID references Applicants(ApplicantID) not null,
ApplicationDate datetime not null default GETDATE(),
CoverLetter text not null);

insert into Companies (CompanyID, CompanyName, ComLocation)
values(101, 'Infotech Solutions', 'Bangalore'),
(102, 'DataBharat Analytics', 'Hyderabad'),
(103, 'DigitalWeb India', 'Pune'),
(104, 'Mumbai Finance Corp', 'Mumbai'),
(105, 'Delhi Tech Services', 'Delhi');

Insert into Applicants (ApplicantID, FirstName, LastName, Email, Phone, AppResume)
values (501, 'Rahul', 'Sharma', 'rahul.sharma@gmail.com', '9876543210', 'Rahul_Resume.pdf'),
(502, 'Priya', 'Patel', 'priya.patel@yahoo.com', '8765432109', 'Priya_CV.docx'),
(503, 'Aarav', 'Singh', 'aarav.singh@outlook.com', '7654321098', NULL),
(504, 'Ananya', 'Gupta', 'ananya.gupta@hotmail.com', '6543210987', 'Ananya_Resume.pdf'),
(505, 'Vihaan', 'Kumar', 'vihaan.kumar@gmail.com', '9432109876', 'Vihaan_Profile.doc');

insert into Jobs (JobID, CompanyID, JobTitle, JobDescription, JobLocation, Salary, JobType, PostedDate, Deadline)
values (1001, 101, 'Software Engineer', 'Develop software applications using Java and Spring Boot. 2+ years experience required.', 'Bangalore', 900000.00, 'Full-time', '2023-10-15', '2023-11-30'),
(1002, 102, 'Data Scientist', 'Analyze large datasets using Python and ML algorithms. Masters degree preferred.', 'Hyderabad', 1200000.00, 'Full-time', '2023-10-20', '2023-12-15'),
(1003, 103, 'Frontend Developer', 'Build responsive UIs using React.js. Freshers may apply.', 'Pune', 600000.00, 'Full-time', '2023-11-01', '2023-12-31'),
(1004, 104, 'Financial Analyst', 'Prepare financial reports and forecasts. CA/CFA qualification required.', 'Mumbai', 800000.00, 'Full-time', '2023-11-05', '2023-12-20'),
(1005, 105, 'DevOps Engineer', 'Implement CI/CD pipelines and cloud infrastructure. AWS certification preferred.', 'Delhi', 1100000.00, 'Contract', '2023-11-10', NULL),
(1006, 101, 'HR Recruiter', 'Manage end-to-end recruitment process. Excellent communication skills required.', 'Bangalore', 500000.00, 'Full-time', '2023-11-15', '2024-01-15');

insert into Applications (ApplicationID, JobID, ApplicantID, ApplicationDate, CoverLetter)
values (2001, 1001, 501, '2023-10-25', 'Dear Hiring Manager, I am excited to apply for the Software Engineer position at Infotech Solutions. With my 3 years of experience...'),
(2002, 1002, 502, '2023-10-28', 'Respected Team, I believe my MSc in Data Science and internship experience make me a strong candidate...'),
(2003, 1003, 503, '2023-11-05', 'Hello, As a recent graduate from Pune University with React.js certification, I would love to...'),
(2004, 1001, 504, '2023-11-10', 'Dear Sir/Madam, Please find my application for the Software Engineer role. I have attached my...'),
(2005, 1004, 505, '2023-11-12', 'To The Recruitment Team, My CA qualification and 2 years at Deloitte have prepared me well...'),
(2006, 1005, 501, '2023-11-15', 'Dear Hiring Team, I am writing to express my interest in the DevOps position. My AWS Solutions Architect...');

Select * from Companies;
Select * from Applicants;
Select * from Jobs;
Select * from Applications;

drop table Companies;
drop table Applicants;
drop table Jobs;
drop table Applications;
