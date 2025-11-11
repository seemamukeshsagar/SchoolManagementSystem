-- Create BookCategoryMaster table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='BookCategoryMaster' AND xtype='U')
BEGIN
    CREATE TABLE [dbo].[BookCategoryMaster] (
        [Id] UNIQUEIDENTIFIER NOT NULL,
        [Name] NVARCHAR(50) NOT NULL,
        [Description] NVARCHAR(150) NULL,
        [IsActive] BIT NOT NULL DEFAULT 1,
        [IsDeleted] BIT NOT NULL DEFAULT 0,
        [CompanyId] UNIQUEIDENTIFIER NOT NULL,
        [SchoolId] UNIQUEIDENTIFIER NOT NULL,
        [CreatedBy] UNIQUEIDENTIFIER NOT NULL,
        [CreatedDate] DATE NOT NULL,
        [ModifiedBy] UNIQUEIDENTIFIER NULL,
        [ModifiedDate] DATETIME NULL,
        [Status] NVARCHAR(10) NOT NULL DEFAULT 'ACT',
        [StatusMessage] NVARCHAR(255) NOT NULL DEFAULT 'Active',
        CONSTRAINT [PK_BookCategoryMaster] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_BookCategoryMaster_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [CompanyMaster]([Id]),
        CONSTRAINT [FK_BookCategoryMaster_SchoolID] FOREIGN KEY ([SchoolId]) REFERENCES [SchoolMaster]([Id]),
        CONSTRAINT [FK_BookCategoryMaster_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [UserDetails]([Id]),
        CONSTRAINT [FK_BookCategoryMaster_ModifiedBy] FOREIGN KEY ([ModifiedBy]) REFERENCES [UserDetails]([Id])
    );
    
    PRINT 'BookCategoryMaster table created successfully.';
END
ELSE
BEGIN
    PRINT 'BookCategoryMaster table already exists.';
END