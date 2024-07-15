USE [VaultN]
GO

/****** Object:  StoredProcedure [dbo].[GetPagedUsers]    Script Date: 7/15/2024 5:34:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[GetPagedUsers]
    @PageSize INT,
    @PageNumber INT
AS
BEGIN
    /* Calculate the offset which will be used in OFFSET clause to skip rows */
    DECLARE @Offset INT;
    SET @Offset = (@PageNumber - 1) * @PageSize;

    SELECT *
    FROM dbo.Users
    ORDER BY Id /* this column should be indexed for better performance*/
    OFFSET @Offset ROWS
    FETCH NEXT @PageSize ROWS ONLY; /* fetch the rows in the page*/
END
GO

