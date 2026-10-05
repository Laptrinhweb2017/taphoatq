USE TapHoa
GO

/*===LoginInfo===*/
CREATE PROC LoginInfoAdd
	@UserID varchar(12),
	@PassWord varchar(20)
AS
BEGIN
	INSERT INTO dbo.LoginInfo (UserID, PassWord)
	OUTPUT Inserted.*
	VALUES(@UserID, @PassWord)
END
GO
CREATE PROC LoginInfoUpdate
	@id int,
	@UserID varchar(12),
	@PassWord varchar(20)
AS
BEGIN
	UPDATE dbo.LoginInfo
	SET
		UserID = @UserID,
		PassWord = @PassWord
	OUTPUT Inserted.*
	WHERE id = @id
END
GO

CREATE PROC LoginInfoDelete
	@id int
AS
BEGIN
	DELETE FROM dbo.LoginInfo
	WHERE id = @id
END
GO
/*===Customer===*/
CREATE PROC CustomerAdd
	@CustomerCode varchar(12),
	@CustomerName nvarchar(250),
	@TaxCode varchar(12),
	@Phone varchar(50),
	@Address nvarchar(250)
AS
BEGIN
	INSERT INTO dbo.Customer (CustomerCode, CustomerName, TaxCode, Phone, Address)
	OUTPUT Inserted.*
	VALUES(@CustomerCode, @CustomerName, @TaxCode, @Phone, @Address)
END
GO
CREATE PROC CustomerUpdate
	@id int,
	@CustomerCode varchar(12),
	@CustomerName nvarchar(250),
	@TaxCode varchar(12),
	@Phone varchar(50),
	@Address nvarchar(250)
AS
BEGIN
	UPDATE dbo.Customer
	SET
		CustomerCode = @CustomerCode,
		CustomerName = @CustomerName,
		TaxCode = @TaxCode,
		Phone = @Phone,
		Address = @Address
	OUTPUT Inserted.*
	WHERE id = @id
END
GO

CREATE PROC CustomerDelete
	@id int
AS
BEGIN
	DELETE FROM dbo.Customer
	WHERE id = @id
END
GO
/*===Employee===*/
CREATE PROC EmployeeAdd
	@FullName nvarchar(100),
	@Email varchar(200),
	@EmloyeeCode varchar(12)
AS
BEGIN
	INSERT INTO dbo.Employee (FullName, Email, EmloyeeCode)
	OUTPUT Inserted.*
	VALUES(@FullName, @Email, @EmloyeeCode)
END
GO
CREATE PROC EmployeeUpdate
	@id int,
	@FullName nvarchar(100),
	@Email varchar(200),
	@EmloyeeCode varchar(12)
AS
BEGIN
	UPDATE dbo.Employee
	SET
		FullName = @FullName,
		Email = @Email,
		EmloyeeCode = @EmloyeeCode
	OUTPUT Inserted.*
	WHERE id = @id
END
GO

CREATE PROC EmployeeDelete
	@id int
AS
BEGIN
	DELETE FROM dbo.Employee
	WHERE id = @id
END
GO
/*===Supplier===*/
CREATE PROC SupplierAdd
	@SupplierCode varchar(12),
	@SupplierName nvarchar(250),
	@Phone varchar(50),
	@Address nvarchar(250)
AS
BEGIN
	INSERT INTO dbo.Supplier (SupplierCode, SupplierName, Phone, Address)
	OUTPUT Inserted.*
	VALUES(@SupplierCode, @SupplierName, @Phone, @Address)
END
GO
CREATE PROC SupplierUpdate
	@id int,
	@SupplierCode varchar(12),
	@SupplierName nvarchar(250),
	@Phone varchar(50),
	@Address nvarchar(250)
AS
BEGIN
	UPDATE dbo.Supplier
	SET
		SupplierCode = @SupplierCode,
		SupplierName = @SupplierName,
		Phone = @Phone,
		Address = @Address
	OUTPUT Inserted.*
	WHERE id = @id
END
GO

CREATE PROC SupplierDelete
	@id int
AS
BEGIN
	DELETE FROM dbo.Supplier
	WHERE id = @id
END
GO
/*===StoreInfo===*/
CREATE PROC StoreInfoAdd
	@StoreName nvarchar(20),
	@Address nvarchar(250),
	@TaxCode varchar(20),
	@Phone varchar(50)
AS
BEGIN
	INSERT INTO dbo.StoreInfo (StoreName, Address, TaxCode, Phone)
	OUTPUT Inserted.*
	VALUES(@StoreName, @Address, @TaxCode, @Phone)
END
GO
CREATE PROC StoreInfoUpdate
	@id int,
	@StoreName nvarchar(20),
	@Address nvarchar(250),
	@TaxCode varchar(20),
	@Phone varchar(50)
AS
BEGIN
	UPDATE dbo.StoreInfo
	SET
		StoreName = @StoreName,
		Address = @Address,
		TaxCode = @TaxCode,
		Phone = @Phone
	OUTPUT Inserted.*
	WHERE id = @id
END
GO

CREATE PROC StoreInfoDelete
	@id int
AS
BEGIN
	DELETE FROM dbo.StoreInfo
	WHERE id = @id
END
GO
/*===Brand===*/
CREATE PROC BrandAdd
	@BrandName nvarchar(255)
AS
BEGIN
	INSERT INTO dbo.Brand (BrandName)
	OUTPUT Inserted.*
	VALUES(@BrandName)
END
GO
CREATE PROC BrandUpdate
	@id int,
	@BrandName nvarchar(255)
AS
BEGIN
	UPDATE dbo.Brand
	SET
		BrandName = @BrandName
	OUTPUT Inserted.*
	WHERE id = @id
END
GO

CREATE PROC BrandDelete
	@id int
AS
BEGIN
	DELETE FROM dbo.Brand
	WHERE id = @id
END
GO
/*===Category===*/
CREATE PROC CategoryAdd
	@CateName nvarchar(250)
AS
BEGIN
	INSERT INTO dbo.Category (CateName)
	OUTPUT Inserted.*
	VALUES(@CateName)
END
GO
CREATE PROC CategoryUpdate
	@id int,
	@CateName nvarchar(250)
AS
BEGIN
	UPDATE dbo.Category
	SET
		CateName = @CateName
	OUTPUT Inserted.*
	WHERE id = @id
END
GO

CREATE PROC CategoryDelete
	@id int
AS
BEGIN
	DELETE FROM dbo.Category
	WHERE id = @id
END
GO
/*===Products===*/
CREATE PROC ProductsAdd
	@ProductCode varchar(12),
	@ProductName nvarchar(250),
	@Image varchar(255),
	@Descript nvarchar(250),
	@CreatedAt datetime,
	@BrandID int,
	@CateID int
AS
BEGIN
	INSERT INTO dbo.Products (ProductCode, ProductName, Image, Descript, CreatedAt, BrandID, CateID)
	OUTPUT Inserted.*
	VALUES(@ProductCode, @ProductName, @Image, @Descript, @CreatedAt, @BrandID, @CateID)
END
GO
CREATE PROC ProductsUpdate
	@id int,
	@ProductCode varchar(12),
	@ProductName nvarchar(250),
	@Image varchar(255),
	@Descript nvarchar(250),
	@CreatedAt datetime,
	@BrandID int,
	@CateID int
AS
BEGIN
	UPDATE dbo.Products
	SET
		ProductCode = @ProductCode,
		ProductName = @ProductName,
		Image = @Image,
		Descript = @Descript,
		CreatedAt = @CreatedAt,
		BrandID = @BrandID,
		CateID = @CateID
	OUTPUT Inserted.*
	WHERE id = @id
END
GO

CREATE PROC ProductsDelete
	@id int
AS
BEGIN
	DELETE FROM dbo.Products
	WHERE id = @id
END
GO
/*===Price===*/
CREATE PROC PriceAdd
	@ProdID int,
	@Price decimal,
	@AppliedDate datetime
AS
BEGIN
	INSERT INTO dbo.Price (ProdID, Price, AppliedDate)
	OUTPUT Inserted.*
	VALUES(@ProdID, @Price, @AppliedDate)
END
GO
CREATE PROC PriceUpdate
	@id int,
	@ProdID int,
	@Price decimal,
	@AppliedDate datetime
AS
BEGIN
	UPDATE dbo.Price
	SET
		ProdID = @ProdID,
		Price = @Price,
		AppliedDate = @AppliedDate
	OUTPUT Inserted.*
	WHERE id = @id
END
GO

CREATE PROC PriceDelete
	@id int
AS
BEGIN
	DELETE FROM dbo.Price
	WHERE id = @id
END
GO
/*===Warehouses===*/
CREATE PROC WarehousesAdd
	@WarehouseName nvarchar(250),
	@Address nvarchar(250)
AS
BEGIN
	INSERT INTO dbo.Warehouses (WarehouseName, Address)
	OUTPUT Inserted.*
	VALUES(@WarehouseName, @Address)
END
GO
CREATE PROC WarehousesUpdate
	@id int,
	@WarehouseName nvarchar(250),
	@Address nvarchar(250)
AS
BEGIN
	UPDATE dbo.Warehouses
	SET
		WarehouseName = @WarehouseName,
		Address = @Address
	OUTPUT Inserted.*
	WHERE id = @id
END
GO

CREATE PROC WarehousesDelete
	@id int
AS
BEGIN
	DELETE FROM dbo.Warehouses
	WHERE id = @id
END
GO
/*===Inventory===*/
CREATE PROC InventoryAdd
	@WarehouseId int,
	@ProductId int,
	@Quantity int
AS
BEGIN
	INSERT INTO dbo.Inventory (WarehouseId, ProductId, Quantity)
	OUTPUT Inserted.*
	VALUES(@WarehouseId, @ProductId, @Quantity)
END
GO
CREATE PROC InventoryUpdate
	@id int,
	@WarehouseId int,
	@ProductId int,
	@Quantity int
AS
BEGIN
	UPDATE dbo.Inventory
	SET
		WarehouseId = @WarehouseId,
		ProductId = @ProductId,
		Quantity = @Quantity
	OUTPUT Inserted.*
	WHERE id = @id
END
GO

CREATE PROC InventoryDelete
	@id int
AS
BEGIN
	DELETE FROM dbo.Inventory
	WHERE id = @id
END
GO
/*===StockInput===*/
CREATE PROC StockInputAdd
	@ReceiptCode varchar(20),
	@ReceivedDate datetime,
	@SupplierId int,
	@EmployeeId int
AS
BEGIN
	INSERT INTO dbo.StockInput (ReceiptCode, ReceivedDate, SupplierId, EmployeeId)
	OUTPUT Inserted.*
	VALUES(@ReceiptCode, @ReceivedDate, @SupplierId, @EmployeeId)
END
GO
CREATE PROC StockInputUpdate
	@id int,
	@ReceiptCode varchar(20),
	@ReceivedDate datetime,
	@SupplierId int,
	@EmployeeId int
AS
BEGIN
	UPDATE dbo.StockInput
	SET
		ReceiptCode = @ReceiptCode,
		ReceivedDate = @ReceivedDate,
		SupplierId = @SupplierId,
		EmployeeId = @EmployeeId
	OUTPUT Inserted.*
	WHERE id = @id
END
GO

CREATE PROC StockInputDelete
	@id int
AS
BEGIN
	DELETE FROM dbo.StockInput
	WHERE id = @id
END
GO
/*===InputDetail===*/
CREATE PROC InputDetailAdd
	@InputId int,
	@ProductId int,
	@Quantity int,
	@UnitPrice decimal
AS
BEGIN
	INSERT INTO dbo.InputDetail (InputId, ProductId, Quantity, UnitPrice)
	OUTPUT Inserted.*
	VALUES(@InputId, @ProductId, @Quantity, @UnitPrice)
END
GO
CREATE PROC InputDetailUpdate
	@id int,
	@InputId int,
	@ProductId int,
	@Quantity int,
	@UnitPrice decimal
AS
BEGIN
	UPDATE dbo.InputDetail
	SET
		InputId = @InputId,
		ProductId = @ProductId,
		Quantity = @Quantity,
		UnitPrice = @UnitPrice
	OUTPUT Inserted.*
	WHERE id = @id
END
GO

CREATE PROC InputDetailDelete
	@id int
AS
BEGIN
	DELETE FROM dbo.InputDetail
	WHERE id = @id
END
GO
/*===OrderStatus===*/
CREATE PROC OrderStatusAdd
	@Descript nvarchar(250)
AS
BEGIN
	INSERT INTO dbo.OrderStatus (Descript)
	OUTPUT Inserted.*
	VALUES(@Descript)
END
GO
CREATE PROC OrderStatusUpdate
	@id int,
	@Descript nvarchar(250)
AS
BEGIN
	UPDATE dbo.OrderStatus
	SET
		Descript = @Descript
	OUTPUT Inserted.*
	WHERE id = @id
END
GO

CREATE PROC OrderStatusDelete
	@id int
AS
BEGIN
	DELETE FROM dbo.OrderStatus
	WHERE id = @id
END
GO
/*===Orders===*/
CREATE PROC OrdersAdd
	@OrderCode varchar(20),
	@CustomerId int,
	@OrderDate datetime,
	@Status int
AS
BEGIN
	INSERT INTO dbo.Orders (OrderCode, CustomerId, OrderDate, Status)
	OUTPUT Inserted.*
	VALUES(@OrderCode, @CustomerId, @OrderDate, @Status)
END
GO
CREATE PROC OrdersUpdate
	@id int,
	@OrderCode varchar(20),
	@CustomerId int,
	@OrderDate datetime,
	@Status int
AS
BEGIN
	UPDATE dbo.Orders
	SET
		OrderCode = @OrderCode,
		CustomerId = @CustomerId,
		OrderDate = @OrderDate,
		Status = @Status
	OUTPUT Inserted.*
	WHERE id = @id
END
GO

CREATE PROC OrdersDelete
	@id int
AS
BEGIN
	DELETE FROM dbo.Orders
	WHERE id = @id
END
GO
/*===OrderDetail===*/
CREATE PROC OrderDetailAdd
	@OrderId int,
	@ProductId int,
	@Quantity int,
	@UnitPrice decimal
AS
BEGIN
	INSERT INTO dbo.OrderDetail (OrderId, ProductId, Quantity, UnitPrice)
	OUTPUT Inserted.*
	VALUES(@OrderId, @ProductId, @Quantity, @UnitPrice)
END
GO
CREATE PROC OrderDetailUpdate
	@id int,
	@OrderId int,
	@ProductId int,
	@Quantity int,
	@UnitPrice decimal
AS
BEGIN
	UPDATE dbo.OrderDetail
	SET
		OrderId = @OrderId,
		ProductId = @ProductId,
		Quantity = @Quantity,
		UnitPrice = @UnitPrice
	OUTPUT Inserted.*
	WHERE id = @id
END
GO

CREATE PROC OrderDetailDelete
	@id int
AS
BEGIN
	DELETE FROM dbo.OrderDetail
	WHERE id = @id
END
GO
/*===StockOutput===*/
CREATE PROC StockOutputAdd
	@OutputCode varchar(20),
	@OutputDate datetime,
	@EmployeeId int,
	@OrderId int
AS
BEGIN
	INSERT INTO dbo.StockOutput (OutputCode, OutputDate, EmployeeId, OrderId)
	OUTPUT Inserted.*
	VALUES(@OutputCode, @OutputDate, @EmployeeId, @OrderId)
END
GO
CREATE PROC StockOutputUpdate
	@id int,
	@OutputCode varchar(20),
	@OutputDate datetime,
	@EmployeeId int,
	@OrderId int
AS
BEGIN
	UPDATE dbo.StockOutput
	SET
		OutputCode = @OutputCode,
		OutputDate = @OutputDate,
		EmployeeId = @EmployeeId,
		OrderId = @OrderId
	OUTPUT Inserted.*
	WHERE id = @id
END
GO

CREATE PROC StockOutputDelete
	@id int
AS
BEGIN
	DELETE FROM dbo.StockOutput
	WHERE id = @id
END
GO
/*===OutputDetail===*/
CREATE PROC OutputDetailAdd
	@OutputId int,
	@ProductId int,
	@Quantity int,
	@UnitPrice decimal
AS
BEGIN
	INSERT INTO dbo.OutputDetail (OutputId, ProductId, Quantity, UnitPrice)
	OUTPUT Inserted.*
	VALUES(@OutputId, @ProductId, @Quantity, @UnitPrice)
END
GO
CREATE PROC OutputDetailUpdate
	@id int,
	@OutputId int,
	@ProductId int,
	@Quantity int,
	@UnitPrice decimal
AS
BEGIN
	UPDATE dbo.OutputDetail
	SET
		OutputId = @OutputId,
		ProductId = @ProductId,
		Quantity = @Quantity,
		UnitPrice = @UnitPrice
	OUTPUT Inserted.*
	WHERE id = @id
END
GO

CREATE PROC OutputDetailDelete
	@id int
AS
BEGIN
	DELETE FROM dbo.OutputDetail
	WHERE id = @id
END
GO
/*===TrackingStatus===*/
CREATE PROC TrackingStatusAdd
	@StatusName nvarchar(100)
AS
BEGIN
	INSERT INTO dbo.TrackingStatus (StatusName)
	OUTPUT Inserted.*
	VALUES(@StatusName)
END
GO
CREATE PROC TrackingStatusUpdate
	@id int,
	@StatusName nvarchar(100)
AS
BEGIN
	UPDATE dbo.TrackingStatus
	SET
		StatusName = @StatusName
	OUTPUT Inserted.*
	WHERE id = @id
END
GO

CREATE PROC TrackingStatusDelete
	@id int
AS
BEGIN
	DELETE FROM dbo.TrackingStatus
	WHERE id = @id
END
GO
/*===POTracking===*/
CREATE PROC POTrackingAdd
	@OrderId int,
	@StatusId int,
	@TrackedAt datetime
AS
BEGIN
	INSERT INTO dbo.POTracking (OrderId, StatusId, TrackedAt)
	OUTPUT Inserted.*
	VALUES(@OrderId, @StatusId, @TrackedAt)
END
GO
CREATE PROC POTrackingUpdate
	@id int,
	@OrderId int,
	@StatusId int,
	@TrackedAt datetime
AS
BEGIN
	UPDATE dbo.POTracking
	SET
		OrderId = @OrderId,
		StatusId = @StatusId,
		TrackedAt = @TrackedAt
	OUTPUT Inserted.*
	WHERE id = @id
END
GO

CREATE PROC POTrackingDelete
	@id int
AS
BEGIN
	DELETE FROM dbo.POTracking
	WHERE id = @id
END
GO
/*===PaymentMethod===*/
CREATE PROC PaymentMethodAdd
	@PayMethod nvarchar(250)
AS
BEGIN
	INSERT INTO dbo.PaymentMethod (PayMethod)
	OUTPUT Inserted.*
	VALUES(@PayMethod)
END
GO
CREATE PROC PaymentMethodUpdate
	@id int,
	@PayMethod nvarchar(250)
AS
BEGIN
	UPDATE dbo.PaymentMethod
	SET
		PayMethod = @PayMethod
	OUTPUT Inserted.*
	WHERE id = @id
END
GO

CREATE PROC PaymentMethodDelete
	@id int
AS
BEGIN
	DELETE FROM dbo.PaymentMethod
	WHERE id = @id
END
GO
/*===Payment===*/
CREATE PROC PaymentAdd
	@OrderId int,
	@TotalAmount decimal,
	@RemainingDebt decimal
AS
BEGIN
	INSERT INTO dbo.Payment (OrderId, TotalAmount, RemainingDebt)
	OUTPUT Inserted.*
	VALUES(@OrderId, @TotalAmount, @RemainingDebt)
END
GO
CREATE PROC PaymentUpdate
	@id int,
	@OrderId int,
	@TotalAmount decimal,
	@RemainingDebt decimal
AS
BEGIN
	UPDATE dbo.Payment
	SET
		OrderId = @OrderId,
		TotalAmount = @TotalAmount,
		RemainingDebt = @RemainingDebt
	OUTPUT Inserted.*
	WHERE id = @id
END
GO

CREATE PROC PaymentDelete
	@id int
AS
BEGIN
	DELETE FROM dbo.Payment
	WHERE id = @id
END
GO
/*===PaymentDetail===*/
CREATE PROC PaymentDetailAdd
	@PaymentId int,
	@PaymentDate datetime,
	@PaymentMethod int,
	@Amount decimal
AS
BEGIN
	INSERT INTO dbo.PaymentDetail (PaymentId, PaymentDate, PaymentMethod, Amount)
	OUTPUT Inserted.*
	VALUES(@PaymentId, @PaymentDate, @PaymentMethod, @Amount)
END
GO
CREATE PROC PaymentDetailUpdate
	@id int,
	@PaymentId int,
	@PaymentDate datetime,
	@PaymentMethod int,
	@Amount decimal
AS
BEGIN
	UPDATE dbo.PaymentDetail
	SET
		PaymentId = @PaymentId,
		PaymentDate = @PaymentDate,
		PaymentMethod = @PaymentMethod,
		Amount = @Amount
	OUTPUT Inserted.*
	WHERE id = @id
END
GO

CREATE PROC PaymentDetailDelete
	@id int
AS
BEGIN
	DELETE FROM dbo.PaymentDetail
	WHERE id = @id
END
GO
