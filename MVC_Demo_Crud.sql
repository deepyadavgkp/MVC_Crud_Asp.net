

use  UserReg

CREATE TABLE Company (
    id int NOT NULL identity(1,1) Primary Key,
    LastName varchar(255) NOT NULL,
    FirstName varchar(255),
    Age int,
	Dob Date,
	Photo Binary
   
);

select * from Company

alter PROCEDURE  insert_C
@FirstName nvarchar(30), 
@LastName nvarchar(10),
@Age int,
	@Dob Date
	
AS
begin
Insert into Company(FirstName,LastName,Age,Dob)
values(@FirstName,@LastName,@Age,@Dob)
end

alter PROCEDURE select_C

AS
BEGIN
    
   SELECT id, FirstName,LastName,Age,Dob from Company
END

CREATE PROCEDURE selectbyid_C
 @id INT
AS
BEGIN
    
   SELECT  FirstName,LastName,Age,Dob,Photo from Company WHERE id = @id;
END


CREATE PROCEDURE UpdateC
@id int,
@FirstName nvarchar(30), 
@LastName nvarchar(10),
@Age int,
	@Dob Date,
	@Photo Binary
AS
BEGIN
	UPDATE Company
	SET FirstName = @FirstName
		
		,LastName = @LastName
		,Age = @Age
		,Dob = @Dob
		,Photo = @Photo
		
	WHERE id = @id
END

CREATE PROCEDURE Delete_C_Id
   @id int
AS
BEGIN
   

    DELETE FROM Company
    WHERE id = @id;
END