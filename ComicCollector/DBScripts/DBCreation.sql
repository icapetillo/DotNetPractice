-- 0. Opciones
SET NOCOUNT ON;
GO


-- 1. Publishers
CREATE TABLE Publishers (
PublisherId INT IDENTITY(1,1) PRIMARY KEY,
Name NVARCHAR(200) NOT NULL UNIQUE,
Country NVARCHAR(100) NULL,
FoundedYear INT NULL
);


-- 2. Series
CREATE TABLE Series (
SeriesId INT IDENTITY(1,1) PRIMARY KEY,
PublisherId INT NOT NULL,
Title NVARCHAR(300) NOT NULL,
Volume INT NULL,
StartYear INT NULL,
EndYear INT NULL,
CONSTRAINT FK_Series_Publisher FOREIGN KEY (PublisherId) REFERENCES Publishers(PublisherId)
);
CREATE INDEX IX_Series_Title ON Series(Title);


-- 3. Creators
CREATE TABLE Creators (
CreatorId INT IDENTITY(1,1) PRIMARY KEY,
Name NVARCHAR(200) NOT NULL,
Country NVARCHAR(100) NULL,
BirthYear INT NULL
);
CREATE INDEX IX_Creators_Name ON Creators(Name);


-- 4. Issues
CREATE TABLE Issues (
IssueId INT IDENTITY(1,1) PRIMARY KEY,
SeriesId INT NOT NULL,
IssueNumber NVARCHAR(20) NOT NULL,
Title NVARCHAR(300) NULL,
ReleaseDate DATE NULL,
Description NVARCHAR(MAX) NULL,
CoverImageUrl NVARCHAR(MAX) NULL,
IsVariant BIT NOT NULL DEFAULT 0,
ParentIssueId INT NULL,
CONSTRAINT FK_Issues_Series FOREIGN KEY (SeriesId) REFERENCES Series(SeriesId),
CONSTRAINT FK_Issues_ParentIssue FOREIGN KEY (ParentIssueId) REFERENCES Issues(IssueId)
);
CREATE INDEX IX_Issues_Series_IssueNumber ON Issues(SeriesId, IssueNumber);

-- 5. Variants
CREATE TABLE Variants (
VariantId INT IDENTITY(1,1) PRIMARY KEY,
IssueId INT NOT NULL,
VariantCode NVARCHAR(50) NULL,
ArtistId INT NULL,
PrintRun INT NULL,
CoverImageUrl NVARCHAR(MAX) NULL,
CONSTRAINT FK_Variants_Issue FOREIGN KEY (IssueId) REFERENCES Issues(IssueId),
CONSTRAINT FK_Variants_Artist FOREIGN KEY (ArtistId) REFERENCES Creators(CreatorId)
);
CREATE INDEX IX_Variants_Issue ON Variants(IssueId);


-- 6. IssueCreators (N:M)
CREATE TABLE IssueCreators (
IssueId INT NOT NULL,
CreatorId INT NOT NULL,
Role NVARCHAR(50) NOT NULL,
CONSTRAINT PK_IssueCreators PRIMARY KEY (IssueId, CreatorId, Role),
CONSTRAINT FK_IssueCreators_Issue FOREIGN KEY (IssueId) REFERENCES Issues(IssueId),
CONSTRAINT FK_IssueCreators_Creator FOREIGN KEY (CreatorId) REFERENCES Creators(CreatorId)
);


-- 7. Tags
CREATE TABLE Tags (
TagId INT IDENTITY(1,1) PRIMARY KEY,
Name NVARCHAR(100) NOT NULL UNIQUE
);


-- 8. IssueTags (N:M)
CREATE TABLE IssueTags (
IssueId INT NOT NULL,
TagId INT NOT NULL,
CONSTRAINT PK_IssueTags PRIMARY KEY (IssueId, TagId),
CONSTRAINT FK_IssueTags_Issue FOREIGN KEY (IssueId) REFERENCES Issues(IssueId),
CONSTRAINT FK_IssueTags_Tag FOREIGN KEY (TagId) REFERENCES Tags(TagId)
);


-- 9. MyCollection
CREATE TABLE MyCollection (
MyComicId INT IDENTITY(1,1) PRIMARY KEY,
IssueId INT NOT NULL,
VariantId INT NULL,
Condition NVARCHAR(50) NULL,
GradingCompany NVARCHAR(50) NULL,
Grade DECIMAL(3,1) NULL,
PurchasePrice DECIMAL(10,2) NULL,
CurrentValue DECIMAL(10,2) NULL,
PurchaseDate DATE NULL,
Signed BIT NOT NULL DEFAULT 0,
SignedBy NVARCHAR(200) NULL,
Notes NVARCHAR(MAX) NULL,
CONSTRAINT FK_MyCollection_Issue FOREIGN KEY (IssueId) REFERENCES Issues(IssueId),
CONSTRAINT FK_MyCollection_Variant FOREIGN KEY (VariantId) REFERENCES Variants(VariantId)
);
CREATE INDEX IX_MyCollection_Issue ON MyCollection(IssueId);


-- 10. StorageLocations
CREATE TABLE StorageLocations (
LocationId INT IDENTITY(1,1) PRIMARY KEY,
Name NVARCHAR(200) NOT NULL,
Description NVARCHAR(MAX) NULL
);


-- 11. MyComicLocations (histórico)
CREATE TABLE MyComicLocations (
MyComicLocationId INT IDENTITY(1,1) PRIMARY KEY,
MyComicId INT NOT NULL,
LocationId INT NOT NULL,
StartDate DATE NOT NULL DEFAULT GETDATE(),
EndDate DATE NULL,
CONSTRAINT FK_MyComicLocations_MyComic FOREIGN KEY (MyComicId) REFERENCES MyCollection(MyComicId),
CONSTRAINT FK_MyComicLocations_Location FOREIGN KEY (LocationId) REFERENCES StorageLocations(LocationId)
);
CREATE INDEX IX_MyComicLocations_MyComic ON MyComicLocations(MyComicId);


-- 12. ReadingStatus
CREATE TABLE ReadingStatus (
ReadingId INT IDENTITY(1,1) PRIMARY KEY,
MyComicId INT NOT NULL,
Status NVARCHAR(20) NOT NULL,
DateRead DATE NULL,
CONSTRAINT FK_ReadingStatus_MyComic FOREIGN KEY (MyComicId) REFERENCES MyCollection(MyComicId)
);


-- 13. WishList
CREATE TABLE WishList (
WishId INT IDENTITY(1,1) PRIMARY KEY,
IssueId INT NOT NULL,
VariantId INT NULL,
Priority INT NULL,
Notes NVARCHAR(MAX) NULL,
CONSTRAINT FK_WishList_Issue FOREIGN KEY (IssueId) REFERENCES Issues(IssueId),
CONSTRAINT FK_WishList_Variant FOREIGN KEY (VariantId) REFERENCES Variants(VariantId)
);


-- 14. Datos de ejemplo mínimos (opcional)
-- INSERT INTO Publishers (Name) VALUES ('Marvel'), ('DC'), ('Image');


GO