# Project Overview
This project is a full-stack Employee Management System built using ASP.NET Core 8 for the backend, HTML, CSS, and JavaScript for the frontend and Use Dapper ORM. The system includes CRUD operations for employees, department management, and employee performance reviews. It also features search, filtering, pagination, and performance optimization using indexed database queries.

Features
============
Backend (ASP.NET Core 8)
=================================
Employee Management: CRUD operations (Create, Read, Update, Soft Delete)

Department Management: Separate entity with manager assignment and budget tracking

Performance Review: Quarterly employee reviews with scoring and notes

Search & Filter: Search employees by name and filter by department and performance score

Pagination: Efficient data loading using OFFSET and LIMIT

Database Optimization: Indexing and optimized queries for large datasets

View for Department Performance: Aggregates employee review scores to calculate department performance

Frontend (HTML, CSS, JavaScript)
================================================
Employee CRUD Operations: Add, update, view, and delete employees

Search & Filter UI: Find employees by name, department, and performance score

Performance Score Display: View average performance scores per department

Database (SQL Server)
====================================
Tables: Employee, Department, PerformanceReview

Indexes: Applied on frequently queried fields for performance improvement

Views: Created for calculating department-wise performance scores

Foreign Key Constraints: Ensuring referential integrity between entities

Instructions
==============================================
I provide the query for making tables and Views , Indexing in the File DataBasescript.txt. Here you you get the querys for my project. You can create a Database and then execute the query and then change the connection string and run the project.

Please feel free to contact me if you face any problem to run the project.
Thank You.
