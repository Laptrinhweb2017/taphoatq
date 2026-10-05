CREATE DATABASE TapHoa
GO
USE TapHoa
GO

-- NAM0207
--NV001

CREATE TABLE LoginInfo (
	[id] INT NOT NULL IDENTITY,
	UserID VARCHAR(12) NOT NULL,
	[PassWord] VARCHAR(20) NOT NULL,
	PRIMARY KEY([id])
);
GO

CREATE TABLE Customer (
	[id] INT NOT NULL IDENTITY,
	[CustomerCode] VARCHAR(12),
	[CustomerName] NVARCHAR(250),
	[TaxCode] VARCHAR(12),
	[Phone] VARCHAR(50),
	[Address] NVARCHAR(250),
	PRIMARY KEY([id])
);
GO

CREATE TABLE Employee (
	[id] INT NOT NULL IDENTITY,
	[FullName] NVARCHAR(100),
	[Email] VARCHAR(200),
	EmloyeeCode VARCHAR(12),
	PRIMARY KEY([id])
);
GO

CREATE TABLE Supplier (
	[id] INT NOT NULL IDENTITY,
	[SupplierCode] VARCHAR(12),
	[SupplierName] NVARCHAR(250),
	[Phone] VARCHAR(50),
	[Address] NVARCHAR(250),
	PRIMARY KEY([id])
);
GO

CREATE TABLE StoreInfo (
	[id] INT NOT NULL IDENTITY,
	[StoreName] NVARCHAR(20) NOT NULL,
	[Address] NVARCHAR(250) NOT NULL,
	[TaxCode] VARCHAR(20) NOT NULL,
	[Phone] VARCHAR(50) NOT NULL,
	PRIMARY KEY([id])
);
GO

CREATE TABLE Brand (
	[id] INT NOT NULL IDENTITY,
	[BrandName] NVARCHAR(255),
	PRIMARY KEY([id])
);
GO

CREATE TABLE Category (
	[id] INT NOT NULL IDENTITY,
	[CateName] NVARCHAR(250),
	PRIMARY KEY([id])
);
GO

CREATE TABLE Products (
	[id] INT NOT NULL IDENTITY,
	[ProductCode] VARCHAR(12),
	[ProductName] NVARCHAR(250),
	[Image] VARCHAR(255),
	[Descript] NVARCHAR(250),
	[CreatedAt] DATETIME,
	BrandID INT FOREIGN KEY REFERENCES Brand(id),
  CateID INT FOREIGN KEY REFERENCES Category(Id)
	PRIMARY KEY([id])
);
GO
CREATE TABLE Price(
  id INT IDENTITY PRIMARY KEY,
  ProdID INT FOREIGN KEY REFERENCES Products(id) NOT NULL,
  Price DECIMAL(9,2) NOT NULL DEFAULT(0),
  AppliedDate DATETIME NOT NULL
)
GO

CREATE TABLE Warehouses (
	[id] INT NOT NULL IDENTITY,
	[WarehouseName] NVARCHAR(250),
	[Address] NVARCHAR(250),
	PRIMARY KEY([id])
);
GO

CREATE TABLE Inventory (
	[id] INT NOT NULL IDENTITY,
	[WarehouseId] INT FOREIGN KEY REFERENCES Warehouses(id),
	[ProductId] INT FOREIGN KEY REFERENCES Products(id),
	[Quantity] INT,
	PRIMARY KEY([id])
);
GO

CREATE TABLE StockInput (
	[id] INT NOT NULL IDENTITY,
	[ReceiptCode] VARCHAR(20),
	[ReceivedDate] DATETIME,
	[SupplierId] INT FOREIGN KEY  REFERENCES Supplier(id),
	[EmployeeId] INT FOREIGN KEY REFERENCES Employee(id),
	PRIMARY KEY([id])
);
GO

CREATE TABLE InputDetail (
	[id] INT NOT NULL IDENTITY,
	[InputId] INT FOREIGN KEY REFERENCES StockInput(Id),
	[ProductId] INT FOREIGN KEY REFERENCES Products(id),
	[Quantity] INT,
	[UnitPrice] DECIMAL(9,2),
	PRIMARY KEY([id])
);
GO

CREATE TABLE OrderStatus(
  id INT IDENTITY PRIMARY KEY,
  Descript NVARCHAR(250) NOT NULL
)
GO

CREATE TABLE Orders (
	[id] INT NOT NULL IDENTITY,
	[OrderCode] VARCHAR(20),
	[CustomerId] INT FOREIGN KEY REFERENCES Customer (Id),
	[OrderDate] DATETIME,
	[Status] INT,
	PRIMARY KEY([id])
);
GO

CREATE TABLE OrderDetail (
	[id] INT NOT NULL IDENTITY,
	[OrderId] INT FOREIGN KEY REFERENCES [Orders] (id),
	[ProductId] INT FOREIGN KEY REFERENCES Products (id),
	[Quantity] INT,
	[UnitPrice] DECIMAL(9,2),
	PRIMARY KEY([id])
);
GO

CREATE TABLE StockOutput (
	[id] INT NOT NULL IDENTITY,
	[OutputCode] VARCHAR(20),
	[OutputDate] DATETIME,
	[EmployeeId] INT FOREIGN KEY REFERENCES Employee(Id),
	[OrderId] INT FOREIGN KEY REFERENCES [Orders](Id),
	PRIMARY KEY([id])
);
GO

CREATE TABLE OutputDetail (
	[id] INT NOT NULL IDENTITY,
	[OutputId] INT FOREIGN KEY REFERENCES StockOutput(Id),
	[ProductId] INT FOREIGN KEY REFERENCES Products(Id),
	[Quantity] INT,
	[UnitPrice] DECIMAL(9,2),
	PRIMARY KEY([id])
);
GO

CREATE TABLE TrackingStatus (
	[id] INT NOT NULL IDENTITY,
	[StatusName] NVARCHAR(100),
	PRIMARY KEY([id])
);
GO

CREATE TABLE POTracking (
	[id] INT NOT NULL IDENTITY,
	[OrderId] INT FOREIGN KEY REFERENCES [Orders] (Id),
	[StatusId] INT FOREIGN KEY REFERENCES TrackingStatus(Id),
	[TrackedAt] DATETIME,
	PRIMARY KEY([id])
);
GO
CREATE TABLE PaymentMethod(
  id INT IDENTITY PRIMARY KEY,
  PayMethod NVARCHAR(250) NOT NULL
)
GO
CREATE TABLE Payment (
	[id] INT NOT NULL IDENTITY,
	[OrderId] INT FOREIGN KEY REFERENCES [Orders] (Id),
	[TotalAmount] DECIMAL(9,2),
	[RemainingDebt] DECIMAL(9,2),
	PRIMARY KEY([id])
);
GO

CREATE TABLE PaymentDetail (
	[id] INT NOT NULL IDENTITY,
	[PaymentId] INT FOREIGN KEY REFERENCES Payment (Id),
	[PaymentDate] DATETIME,
	[PaymentMethod] INT FOREIGN KEY REFERENCES PaymentMethod(id),
	[Amount] DECIMAL(9,2),
	PRIMARY KEY([id])
);
GO
