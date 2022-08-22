--Obtener la lista de precios de todos los productos
SELECT * FROM Product

--Obtener la lista de productos cuya existencia en el inventario haya llegado al mínimo permitido (5 unidades)SELECT p.IdProduct, p.ProductName, i.Quantity FROM product p 	JOIN Inventory i ON i.IdProduct=p.IdProduct 	WHERE i.Quantity=5 --Obtener una lista de clientes no mayores de 35 años que hayan realizado compras entre el 1 de febrero de 2000 y el 25 de mayo de 2000
SELECT DISTINCT	c.LastName, c.FirstName, DATEDIFF(YEAR, BornDay, GETDATE()) as Age
FROM Customer c
LEFT JOIN Bill b ON c.IdCustomer = b.IdCustomer
WHERE (b.BillDate BETWEEN '2000-02-01' AND '2000-05-25') 
AND DATEDIFF(YEAR, BornDay, GETDATE()) < 35;

--Obtener el valor total vendido por cada producto en el año 2000
select distinct p.ProductName, sum(p.price * bd.Quantity) as valorTotal, bd.Quantity, b.BillDate from Bill b, Product p join BillDetail bd on p.IdProduct=bd.IdProduct
WHERE b.BillDate BETWEEN '2000-02-01' AND '2000-05-25'
GROUP BY p.ProductName, bd.Quantity, b.BillDate

--Obtener la última fecha de compra de un cliente y según su frecuencia de compra estimar en qué fecha podría volver a comprar.
SELECT c.FirstName, 
	MIN(b.BillDate) as 'Primera Compra',
	MAX(b.BillDate)as 'Ultima Compra',
	COUNT(b.IdBill)as 'Total compras realizadas',	
	DATEADD(DAY, (DATEDIFF(DAY, Min(b.BillDate), MAX(b.BillDate)) / COUNT(b.IdBill)), MAX(b.BillDate)) as 'Fecha estimada de proxima compra'
	FROM Customer c
	LEFT JOIN Bill b ON c.IdCustomer = b.IdCustomer
	GROUP BY c.IdCustomer, c.FirstName;