Create procedure [dbo].[BarcodeEntry]
(@Item_no int,@Barcode Varchar(100),@MTBarcode varchar(100),@qty int,@rate numeric(18,2))
As
Begin
  insert into barcode_Table(item_no,Barcode,MTBarcode,qty,rate) values(@Item_no,@Barcode,@MTBarcode,@qty,@rate)
End