--Read All Eemployees
CREATE PROC [DBO].[usp_Get_Employees]
AS
BEGIN
	SELECT Id, FirstName, LastName, DateOfBirth, Email, Salary FROM DBO.Employees WITH (NOLOCK)
END

--GetById
CREATE PROC [DBO].[usp_Get_EmployeeById]
(
	@Id INT
)
AS
BEGIN
	SELECT Id, FirstName, LastName, DateOfBirth, Email, Salary FROM DBO.Employees WITH (NOLOCK)
		WHERE Id = @Id
END

--Insert
CREATE PROC [DBO].[usp_Insert_Employee]
(
	@FirstName VARCHAR(50), 
	@LastName VARCHAR(50), 
	@DateOfBirth DATE, 
	@Email VARCHAR(50), 
	@Salary FLOAT
)
AS
BEGIN
	BEGIN TRAN
		BEGIN TRY
			INSERT INTO DBO.Employees(FirstName, LastName, DateOfBirth, Email, Salary)
				VALUES(@FirstName, @LastName, @DateOfBirth, @Email, @Salary) 
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN
		END CATCH
	COMMIT TRAN
END

--Update
CREATE PROC [DBO].[usp_Update_Employee]
(
	@Id INT,
	@FirstName VARCHAR(50), 
	@LastName VARCHAR(50), 
	@DateOfBirth DATE, 
	@Email VARCHAR(50), 
	@Salary FLOAT
)
AS
BEGIN
DECLARE @RowCount INT = 0
	BEGIN TRY
		SET @RowCount = (SELECT COUNT(1) FROM DBO.Employees WITH (NOLOCK) WHERE Id = @Id)
			
		IF(@RowCount > 0)
			BEGIN
				BEGIN TRAN
					UPDATE DBO.Employees SET 
						FirstName = @FirstName, 
						LastName = @LastName, 
						DateOfBirth = @DateOfBirth, 
						Email = @Email, 
						Salary = @Salary
					WHERE Id = @Id
				COMMIT TRAN
			END
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH
END

--Delete
CREATE PROC [DBO].[usp_Delete_Employee]
(
	@Id INT
)
AS
BEGIN
DECLARE @RowCount INT = 0
	BEGIN TRY
		SET @RowCount = (SELECT COUNT(1) FROM DBO.Employees WITH (NOLOCK) WHERE Id = @Id)
			
		IF(@RowCount > 0)
			BEGIN
				BEGIN TRAN
					DELETE FROM DBO.Employees
						WHERE Id = @Id
				COMMIT TRAN
			END
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH
END