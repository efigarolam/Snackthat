-- phpMyAdmin SQL Dump
-- version 3.4.5
-- http://www.phpmyadmin.net
--
-- Servidor: localhost
-- Tiempo de generación: 30-05-2012 a las 19:49:26
-- Versión del servidor: 5.5.16
-- Versión de PHP: 5.3.8

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Base de datos: `snackthat`
--

DELIMITER $$
--
-- Procedimientos
--
CREATE DEFINER=`root`@`localhost` PROCEDURE `assignProductToSeller`(
_idSeller INT(11),
_idProduct INT(11),
_Amount INT(11)
)
BEGIN
    INSERT INTO productsseller (idSeller, idProduct, Amount) VALUES (_idSeller, _idProduct, _Amount);

    UPDATE products SET Amount = (Amount - _Amount) WHERE idProduct = _idProduct;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `deleteCustomerByID`(
_idCustomer INT(11)
)
BEGIN
    IF(EXISTS(SELECT idCustomer FROM customers WHERE idCustomer = _idCustomer)) THEN
        DELETE FROM customers WHERE idCustomer = _idCustomer;
        SELECT TRUE AS Customer_Deleted;
    ELSE
        SELECT TRUE AS Customer_Not_Exists;
    END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `deleteProductByID`(
_idProduct INT(11)
)
BEGIN
    IF(EXISTS(SELECT idProduct FROM products WHERE idProduct = _idProduct)) THEN
        DELETE FROM products WHERE idProduct = _idProduct;
        SELECT TRUE AS Product_Deleted;
    ELSE
        SELECT TRUE AS Product_Not_Exists;
    END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `deleteRouteByID`(
_idRoute INT(11)
)
BEGIN
    IF(EXISTS(SELECT idRoute FROM routes WHERE idRoute = _idRoute)) THEN
        DELETE FROM routes WHERE idRoute = _idRoute;
        SELECT TRUE AS Route_Deleted;
    ELSE
        SELECT TRUE AS Route_Not_Exists;
    END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `deleteSellByID`(
_idSell INT(11)
)
BEGIN
    DECLARE _idProduct INT(11);
    DECLARE _Amount INT(11);
    DECLARE _Action INT DEFAULT 0;
    DECLARE _Products CURSOR FOR SELECT idProduct, Amount FROM productssell WHERE idSell = _idSell;
    DECLARE CONTINUE HANDLER FOR NOT FOUND SET _Action = 1;      
    
    IF(EXISTS(SELECT idSell FROM sells WHERE idSell = _idSell)) THEN
        OPEN _Products;
        read_loop: LOOP
            FETCH _Products INTO _idProduct, _Amount;
            IF(_Action = 1) THEN
                LEAVE read_loop;
            END IF;
            UPDATE products SET Amount = (Amount + _Amount) WHERE idProduct = _idProduct;        
        END LOOP;
        
        DELETE FROM sells WHERE idSell = _idSell;  
        SELECT TRUE AS Sell_Deleted;
    ELSE
        SELECT TRUE AS Sell_Not_Exists;
    END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `deleteUserByID`(
_idUser INT(11)
)
BEGIN
    IF(EXISTS(SELECT idUser FROM users WHERE idUser = _idUser)) THEN
        DELETE FROM users WHERE idUser = _idUser;
        SELECT TRUE AS User_Deleted;
    ELSE
        SELECT TRUE AS User_Not_Exists;
    END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getAllCustomers`()
BEGIN
    SELECT idCustomer AS ID, CONCAT(Name, ' ', LastName) AS Nombre, Email AS Correo, RFC
    FROM customers;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getAllProducts`()
BEGIN
    SELECT products.idProduct AS ID, products.Name AS Nombre, presentations.Presentation AS Presentacion, products.Amount AS Cantidad, products.Price AS Precio
    FROM products INNER JOIN presentations ON products.idPresentation = presentations.idPresentation;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getAllRoutes`()
BEGIN
    SELECT idRoute AS ID, Name AS Nombre
    FROM routes;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getAllSellers`()
BEGIN
	SELECT users.idUser AS ID, CONCAT(users.Name, ' ', users.LastName) AS Nombre
	FROM privileges INNER JOIN users ON privileges.idPrivilege = users.idPrivilege
	WHERE users.idPrivilege = 4;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getAllSellersToWS`(
)
BEGIN
    SELECT *
    FROM users
    WHERE idPrivilege = 4;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getAllUsers`()
BEGIN
    SELECT users.idUser AS ID, CONCAT(users.Name, ' ', users.LastName) AS Nombre, users.Username AS Usuario, users.Email, privileges.Privilege AS Privilegio
    FROM privileges INNER JOIN users ON privileges.idPrivilege = users.idPrivilege
    ORDER BY idUser ASC;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getAssignedRoutes`()
BEGIN
	SELECT 
		routes.Name AS Ruta,
		CONCAT(users.Name, ' ', users.LastName) AS Nombre
	FROM
		routes
			INNER JOIN
		sellerroutes ON routes.idRoute = sellerroutes.idRoute
			INNER JOIN
		users ON sellerroutes.idSeller = users.idUser
	WHERE
		(sellerroutes.idSellerRoute > 0);
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getCustomerAddresses`(
_idCustomer INT(11)
)
BEGIN
	SELECT Address AS Direccion, Phone AS Telefono FROM addressescustomer WHERE idCustomer = _idCustomer;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getCustomerByID`(
_idCustomer INT(11)
)
BEGIN
    IF(EXISTS(SELECT idCustomer FROM customers WHERE idCustomer = _idCustomer)) THEN
        SELECT idCustomer AS ID, Name AS Nombre, LastName AS Apellido, Email AS Correo, RFC
        FROM customers
        WHERE idCustomer = _idCustomer;
    ELSE
        SELECT TRUE AS Customer_Not_Exists;
    END IF;
        
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getCustomersAndAddressesByRoute`(
_idRoute INT(11)
)
BEGIN
	SELECT 
		addressescustomer.idAddressesCustomer AS `ID Direccion`,
		CONCAT(customers.Name, " ", customers.LastName) AS Nombre,
		addressescustomer.Address AS Direccion
	FROM
		addressescustomer
			INNER JOIN
		customersroutes ON addressescustomer.idAddressesCustomer = customersroutes.idAddressesCustomer
			INNER JOIN
		routes ON customersroutes.idRoute = routes.idRoute
			INNER JOIN
		customers ON addressescustomer.idCustomer = customers.idCustomer
	WHERE
		(routes.idRoute = _idRoute);
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getCustomersAndAddressesToEdit`(
_idRoute INT(11)
)
BEGIN
	SELECT 
		addressescustomer.idAddressesCustomer AS `ID Direccion`,
		CONCAT(customers.Name, ' ', customers.LastName) AS Nombre,
		addressescustomer.Address AS Direccion
	FROM
		addressescustomer
			INNER JOIN
		customersroutes ON addressescustomer.idAddressesCustomer = customersroutes.idAddressesCustomer
			INNER JOIN
		routes ON customersroutes.idRoute = routes.idRoute
			INNER JOIN
		customers ON addressescustomer.idCustomer = customers.idCustomer
	WHERE
		(routes.idRoute = _idRoute) 
	UNION SELECT 
		addressescustomer.idAddressesCustomer AS `ID Direccion`,
		CONCAT(customers.Name, ' ', customers.LastName) AS Nombre,
		addressescustomer.Address AS Direccion
	FROM
		addressescustomer
			INNER JOIN
		customers ON addressescustomer.idCustomer = customers.idCustomer
	WHERE
		(addressescustomer.idAddressesCustomer NOT IN (SELECT 
				idAddressesCustomer
			FROM
				customersroutes));
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getCustomersAndAddressesToSave`()
BEGIN
    SELECT addressescustomer.idAddressesCustomer AS `ID Direccion`, CONCAT(customers.Name, ' ', customers.LastName) AS Nombre, addressescustomer.Address AS Direccion
    FROM addressescustomer INNER JOIN customers ON addressescustomer.idCustomer = customers.idCustomer
    WHERE (addressescustomer.idAddressesCustomer NOT IN (SELECT idAddressesCustomer FROM customersroutes));
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getCustomers_CustomersRoutes_AddressesCustomer_ByRoute`(
_idRoute INT(11))
BEGIN
    IF(EXISTS(SELECT idRoute FROM customersroutes WHERE idRoute = _idRoute)) THEN
        SELECT customers.idCustomer AS Cliente1, customers.Name AS Cliente2, customers.LastName AS Cliente3, customers.Email AS Cliente4, customers.RFC AS Cliente5, addressescustomer.idAddressesCustomer AS Direccion1, addressescustomer.idCustomer AS Direccion2, addressescustomer.Address AS Direccion3, addressescustomer.Phone AS Direccion4, customersroutes.idCustomersRoute AS Ruta1, customersroutes.idRoute AS Ruta2, customersroutes.idAddressesCustomer AS Ruta3, customersroutes.Estimated_Time AS Ruta4 
        FROM addressescustomer INNER JOIN customers ON addressescustomer.idCustomer = customers.idCustomer INNER JOIN customersroutes ON addressescustomer.idAddressesCustomer = customersroutes.idAddressesCustomer 
        WHERE (customersroutes.idRoute = _idRoute);
    ELSE
        SELECT TRUE AS Route_Not_Exists;
    END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getCustomerToEdit`(
_idCustomer INT(11)
)
BEGIN
    IF(EXISTS(SELECT idCustomer FROM customers WHERE idCustomer = _idCustomer)) THEN
        SELECT * FROM customers WHERE idCustomer = _idCustomer;
    ELSE
        SELECT TRUE AS Customer_Not_Exists;
    END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getExpiredProducts`()
BEGIN
    SELECT 
        idExpiredProduct AS ID,
        Name AS Nombre,
        Amount AS Cantidad,
        Price AS Precio,
        Expiration_Date AS `Fecha de Caducidad`
    FROM
        expiredproducts;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getLastCustomers`()
BEGIN
    SELECT idCustomer AS ID, CONCAT(Name, ' ', LastName) AS Nombre, Email AS Correo, RFC
    FROM customers ORDER BY idCustomer DESC LIMIT 5;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getLastProducts`()
BEGIN
    SELECT products.idProduct AS ID, products.Name AS Nombre, presentations.Presentation AS Presentacion, products.Amount AS Cantidad, products.Price AS Precio
    FROM products INNER JOIN presentations ON products.idPresentation = presentations.idPresentation ORDER BY idProduct DESC LIMIT 5;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getLastRoutes`()
BEGIN
    SELECT idRoute AS ID, Name AS Nombre
    FROM routes ORDER BY idRoute DESC LIMIT 5;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getLastUsers`()
BEGIN
    SELECT users.idUser AS ID, CONCAT(users.Name, ' ', users.LastName) AS Nombre, users.Username AS Usuario, users.Email, privileges.Privilege AS Privilegio
    FROM privileges INNER JOIN users ON privileges.idPrivilege = users.idPrivilege
    ORDER BY idUser DESC LIMIT 5;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getPrivilegeByUser`(
_Username VARCHAR(45)
)
BEGIN
	SELECT privileges.Privilege
	FROM privileges INNER JOIN users ON privileges.idPrivilege = users.idPrivilege
	WHERE (users.Username = _Username);
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getProductByID`(
_idProduct INT(11)
)
BEGIN
    IF(EXISTS(SELECT idProduct FROM products WHERE idProduct = _idProduct)) THEN
        SELECT products.idProduct AS ID, products.Name AS Nombre, products.Description AS Descripcion, presentations.Presentation AS Presentacion, products.Amount AS Cantidad, products.Price AS Precio, products.First_Date AS `Fecha Ingreso`, products.Expiration_Date AS `Fecha de Caducidad`
        FROM products INNER JOIN presentations ON products.idPresentation = presentations.idPresentation
        WHERE (products.idProduct = _idProduct);
    ELSE
        SELECT TRUE AS Product_Not_Exists;
    END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getProductsBySeller`(
_idSeller int(11))
BEGIN
    IF(EXISTS(SELECT idUser FROM users WHERE idUser = _idSeller)) THEN
        SELECT products.idProduct AS ID, products.Name AS Nombre, products.idPresentation As Presentacion, products.Price AS Precio, products.Description AS Descripcion, productsseller.Amount AS Cantidad
        FROM products INNER JOIN productsseller ON products.idProduct = productsseller.idProduct
        WHERE (productsseller.idSeller = _idSeller);
   ELSE
       SELECT TRUE AS Seller_Not_Exists;
   END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getProductsSell`(
_idSell INT(11)
)
BEGIN
    SELECT 
        productssell.idProductsSell AS ID,
        products.Name AS Nombre,
        presentations.Presentation AS Presentacion,
        products.Price AS Precio,
        productssell.Amount AS Cantidad,
        productssell.Total AS Subtotal
    FROM
        presentations
            INNER JOIN
        products ON presentations.idPresentation = products.idPresentation
            INNER JOIN
        productssell ON products.idProduct = productssell.idProduct
    WHERE
        productssell.idSell = _idSell;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getProductToEdit`(
_idProduct INT(11)
)
BEGIN
    IF(EXISTS(SELECT idProduct FROM products WHERE idProduct = _idProduct)) THEN
        SELECT * FROM products WHERE idProduct = _idProduct;
    ELSE
        SELECT TRUE AS Product_Not_Exists;
    END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getRouteByID`(
_idRoute INT(11)
)
BEGIN
    IF(EXISTS(SELECT idRoute FROM routes WHERE idRoute = _idRoute)) THEN
        SELECT 
            routes.idRoute AS ID,
            routes.Name AS Nombre,
            CONCAT(users.Name, ' ', users.LastName) AS Vendedor
        FROM
            routes
                LEFT OUTER JOIN
            sellerroutes ON routes.idRoute = sellerroutes.idRoute
                LEFT OUTER JOIN
            users ON sellerroutes.idSeller = users.idUser
        WHERE
            (routes.idRoute = _idRoute);
    ELSE
        SELECT TRUE AS Route_Not_Exists;
    END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getRouteBySeller`(
_idSeller INT(11))
BEGIN
    IF(EXISTS(SELECT idSeller FROM sellerroutes WHERE idSeller = _idSeller)) THEN
        SELECT routes.idRoute, routes.Name FROM routes INNER JOIN sellerroutes ON routes.idRoute = sellerroutes.idRoute WHERE (sellerroutes.idSeller = _idSeller);
    ELSE
        SELECT TRUE AS Seller_Not_Exist;
    END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getRouteToEdit`(
_idRoute INT(11)
)
BEGIN
    IF(EXISTS(SELECT idRoute FROM routes WHERE idRoute = _idRoute)) THEN
        SELECT * FROM routes WHERE idRoute = _idRoute;
    ELSE
        SELECT TRUE AS Route_Not_Exists;
    END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getSellByID`(
_idSell INT(11)
)
BEGIN
    IF(EXISTS(SELECT idSell FROM sells WHERE idSell = _idSell)) THEN
        SELECT 
            sells.idSell AS ID,
            CONCAT(customers.Name, ' ', customers.LastName) AS Cliente,
            CONCAT(users.Name, ' ', users.LastName) AS Vendedor,
            sells.Amount AS Cantidad,
            sells.Total,
            sells.Start_Date AS Fecha
        FROM
            sells
                INNER JOIN
            customers ON sells.idCustomer = customers.idCustomer
                INNER JOIN
            users ON sells.idSeller = users.idUser
        WHERE
            sells.idSell = _idSell;
    ELSE
        SELECT TRUE AS Sell_Not_Exists;
    END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getSellsToView`()
BEGIN
    SELECT 
        sells.idSell AS ID,
        CONCAT(customers.Name, ' ', customers.LastName) AS Cliente,
        CONCAT(users.Name, ' ', users.LastName) AS Vendedor,
        sells.Amount AS Cantidad,
        sells.Total,
        sells.Start_Date AS Fecha
    FROM
        sells
            INNER JOIN
        customers ON sells.idCustomer = customers.idCustomer
            INNER JOIN
        users ON sells.idSeller = users.idUser;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getUserByID`(
_idUser INT(11)
)
BEGIN
    IF(EXISTS(SELECT idUser FROM users WHERE idUser = _idUser)) THEN
        SELECT users.idUser AS ID, users.Name AS Nombre, users.LastName AS Apellidos, users.Username AS Usuario, users.Email AS Correo, users.Phone AS Telefono, users.Address AS Direccion, users.RFC, privileges.Privilege AS Privilegio
        FROM users INNER JOIN privileges ON users.idPrivilege = privileges.idPrivilege
        WHERE idUser = _idUser;
    ELSE
        SELECT TRUE AS User_Not_Exists;
    END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getUserByUsername`(
_Username VARCHAR(45)
)
BEGIN
    IF(EXISTS(SELECT idUser FROM users WHERE Username = _Username)) THEN
        SELECT * FROM users WHERE Username = _Username;
    ELSE
        SELECT TRUE AS User_Not_Exists;
    END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getUserToEdit`(
_idUser INT(11)
)
BEGIN
    IF(EXISTS(SELECT idUser FROM users WHERE idUser = _idUser)) THEN
        SELECT * FROM users WHERE idUser = _idUser;
    ELSE
        SELECT TRUE AS User_Not_Exists;
    END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `login`(
_Username VARCHAR(45),
_Password VARCHAR(45)
)
BEGIN
	SELECT idUser FROM users WHERE (Username = _Username) AND (Password = _Password);
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `reportsByExpired`()
BEGIN
    SELECT Name AS Nombre, Amount AS Cantidad, 
    CONCAT(
                (SELECT
                CASE DAYNAME(Expiration_Date) WHEN 'Sunday' THEN 'Domingo' 
                WHEN 'Monday' THEN 'Lunes' 
                WHEN 'Tuesday' THEN 'Martes' 
                WHEN 'Wednesday' THEN 'Miercoles' 
                WHEN 'Thursday' THEN 'Jueves' 
                WHEN 'Friday' THEN 'Viernes' 
                WHEN 'Saturday' THEN 'Sabado' 
                END), 
                ' ', DAY(Expiration_Date), ' ', 
                (SELECT 
                CASE MONTH(Expiration_Date) WHEN 1 THEN 'Enero' 
                WHEN 2 THEN 'Febrero' 
                WHEN 3 THEN 'Marzo' 
                WHEN 4 THEN 'Abril' 
                WHEN 5 THEN 'Mayo' 
                WHEN 6 THEN 'Junio' 
                WHEN 7 THEN 'Julio' 
                WHEN 8 THEN 'Agosto' 
                WHEN 9 THEN 'Septiembre' 
                WHEN 10 THEN 'Octubre' 
                WHEN 11 THEN 'Noviembre' 
                WHEN 12 THEN 'Diciembre' 
                END), 
            ' del ', YEAR(Expiration_Date)) AS Fecha
    FROM snackthat.expiredproducts;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `reportsByMonth`(
_monthInt INT(11))
BEGIN
    IF(_monthInt > 0 && _monthInt < 13) THEN
        SELECT customers.Name AS Cliente, CONCAT(users.Name, ' ', users.LastName) AS Vendedor, products.Name AS Producto, 
            routes.Name AS Ruta, productssell.Amount AS Cantidad, productssell.Total AS Total, 
            (SELECT 
                CASE MONTH(reports.Sell_Date) WHEN 1 THEN 'Enero' 
                WHEN 2 THEN 'Febrero' 
                WHEN 3 THEN 'Marzo' 
                WHEN 4 THEN 'Abril' 
                WHEN 5 THEN 'Mayo' 
                WHEN 6 THEN 'Junio' 
                WHEN 7 THEN 'Julio' 
                WHEN 8 THEN 'Agosto' 
                WHEN 9 THEN 'Septiembre' 
                WHEN 10 THEN 'Octubre' 
                WHEN 11 THEN 'Noviembre' 
                WHEN 12 THEN 'Diciembre' 
            END) AS Fecha 
        FROM reports 
        INNER JOIN users ON reports.idSeller = users.idUser 
        INNER JOIN products ON reports.idProduct = products.idProduct 
        INNER JOIN routes ON reports.idRoute = routes.idRoute 
        INNER JOIN productssell ON products.idProduct = productssell.idProduct 
        INNER JOIN sells ON productssell.idSell = sells.idSell 
        INNER JOIN customers ON sells.idCustomer = customers.idCustomer 
        WHERE MONTH(reports.Sell_Date) = _monthInt;
    ELSEIF(_monthInt = 0) THEN
        SELECT customers.Name AS Cliente, CONCAT(users.Name, ' ', users.LastName) AS Vendedor, products.Name AS Producto, 
            routes.Name AS Ruta, productssell.Amount AS Cantidad, productssell.Total AS Total, 
            (SELECT 
                CASE MONTH(reports.Sell_Date) WHEN 1 THEN 'Enero' 
                WHEN 2 THEN 'Febrero' 
                WHEN 3 THEN 'Marzo' 
                WHEN 4 THEN 'Abril' 
                WHEN 5 THEN 'Mayo' 
                WHEN 6 THEN 'Junio' 
                WHEN 7 THEN 'Julio' 
                WHEN 8 THEN 'Agosto' 
                WHEN 9 THEN 'Septiembre' 
                WHEN 10 THEN 'Octubre' 
                WHEN 11 THEN 'Noviembre' 
                WHEN 12 THEN 'Diciembre' 
            END) AS Fecha 
        FROM reports 
        INNER JOIN users ON reports.idSeller = users.idUser 
        INNER JOIN products ON reports.idProduct = products.idProduct 
        INNER JOIN routes ON reports.idRoute = routes.idRoute 
        INNER JOIN productssell ON products.idProduct = productssell.idProduct 
        INNER JOIN sells ON productssell.idSell = sells.idSell 
        INNER JOIN customers ON sells.idCustomer = customers.idCustomer 
        ORDER BY reports.Sell_Date ASC;
    END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `reportsByProduct`(
_idProduct INT(11))
BEGIN
    IF(_idProduct > 0) THEN
        SELECT customers.Name AS Cliente, CONCAT(users.Name, ' ', users.LastName) AS Vendedor, products.Name AS Producto, 
            routes.Name AS Ruta, productssell.Amount AS Cantidad, productssell.Total AS Total, 
            CONCAT(
                (SELECT
                CASE DAYNAME(reports.Sell_Date) WHEN 'Sunday' THEN 'Domingo' 
                WHEN 'Monday' THEN 'Lunes' 
                WHEN 'Tuesday' THEN 'Martes' 
                WHEN 'Wednesday' THEN 'Miercoles' 
                WHEN 'Thursday' THEN 'Jueves' 
                WHEN 'Friday' THEN 'Viernes' 
                WHEN 'Saturday' THEN 'Sabado' 
                END), 
                ' ', DAY(reports.Sell_Date), ' ', 
                (SELECT 
                CASE MONTH(reports.Sell_Date) WHEN 1 THEN 'Enero' 
                WHEN 2 THEN 'Febrero' 
                WHEN 3 THEN 'Marzo' 
                WHEN 4 THEN 'Abril' 
                WHEN 5 THEN 'Mayo' 
                WHEN 6 THEN 'Junio' 
                WHEN 7 THEN 'Julio' 
                WHEN 8 THEN 'Agosto' 
                WHEN 9 THEN 'Septiembre' 
                WHEN 10 THEN 'Octubre' 
                WHEN 11 THEN 'Noviembre' 
                WHEN 12 THEN 'Diciembre' 
                END), 
            ' del ', YEAR(reports.Sell_Date)) AS Fecha 
        FROM reports 
        INNER JOIN users ON reports.idSeller = users.idUser 
        INNER JOIN products ON reports.idProduct = products.idProduct 
        INNER JOIN routes ON reports.idRoute = routes.idRoute 
        INNER JOIN productssell ON products.idProduct = productssell.idProduct 
        INNER JOIN sells ON productssell.idSell = sells.idSell 
        INNER JOIN customers ON sells.idCustomer = customers.idCustomer 
        WHERE products.idProduct = _idProduct;
    ELSEIF(_idProduct = 0) THEN
        SELECT customers.Name AS Cliente, CONCAT(users.Name, ' ', users.LastName) AS Vendedor, products.Name AS Producto, 
            routes.Name AS Ruta, productssell.Amount AS Cantidad, productssell.Total AS Total, 
            CONCAT(
                (SELECT
                CASE DAYNAME(reports.Sell_Date) WHEN 'Sunday' THEN 'Domingo' 
                WHEN 'Monday' THEN 'Lunes' 
                WHEN 'Tuesday' THEN 'Martes' 
                WHEN 'Wednesday' THEN 'Miercoles' 
                WHEN 'Thursday' THEN 'Jueves' 
                WHEN 'Friday' THEN 'Viernes' 
                WHEN 'Saturday' THEN 'Sabado' 
                END), 
                ' ', DAY(reports.Sell_Date), ' ', 
                (SELECT 
                CASE MONTH(reports.Sell_Date) WHEN 1 THEN 'Enero' 
                WHEN 2 THEN 'Febrero' 
                WHEN 3 THEN 'Marzo' 
                WHEN 4 THEN 'Abril' 
                WHEN 5 THEN 'Mayo' 
                WHEN 6 THEN 'Junio' 
                WHEN 7 THEN 'Julio' 
                WHEN 8 THEN 'Agosto' 
                WHEN 9 THEN 'Septiembre' 
                WHEN 10 THEN 'Octubre' 
                WHEN 11 THEN 'Noviembre' 
                WHEN 12 THEN 'Diciembre' 
                END), 
            ' del ', YEAR(reports.Sell_Date)) AS Fecha 
        FROM reports 
        INNER JOIN users ON reports.idSeller = users.idUser 
        INNER JOIN products ON reports.idProduct = products.idProduct 
        INNER JOIN routes ON reports.idRoute = routes.idRoute 
        INNER JOIN productssell ON products.idProduct = productssell.idProduct 
        INNER JOIN sells ON productssell.idSell = sells.idSell 
        INNER JOIN customers ON sells.idCustomer = customers.idCustomer;
    END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `reportsByRoute`(
_idRoute INT(11)
)
BEGIN
    IF(_idRoute > 0) THEN
        SELECT customers.Name AS Cliente, CONCAT(users.Name, ' ', users.LastName) AS Vendedor, products.Name AS Producto, 
            routes.Name AS Ruta, productssell.Amount AS Cantidad, productssell.Total AS Total, 
            CONCAT(
                (SELECT
                CASE DAYNAME(reports.Sell_Date) WHEN 'Sunday' THEN 'Domingo' 
                WHEN 'Monday' THEN 'Lunes' 
                WHEN 'Tuesday' THEN 'Martes' 
                WHEN 'Wednesday' THEN 'Miercoles' 
                WHEN 'Thursday' THEN 'Jueves' 
                WHEN 'Friday' THEN 'Viernes' 
                WHEN 'Saturday' THEN 'Sabado' 
                END), 
                ' ', DAY(reports.Sell_Date), ' ', 
                (SELECT 
                CASE MONTH(reports.Sell_Date) WHEN 1 THEN 'Enero' 
                WHEN 2 THEN 'Febrero' 
                WHEN 3 THEN 'Marzo' 
                WHEN 4 THEN 'Abril' 
                WHEN 5 THEN 'Mayo' 
                WHEN 6 THEN 'Junio' 
                WHEN 7 THEN 'Julio' 
                WHEN 8 THEN 'Agosto' 
                WHEN 9 THEN 'Septiembre' 
                WHEN 10 THEN 'Octubre' 
                WHEN 11 THEN 'Noviembre' 
                WHEN 12 THEN 'Diciembre' 
                END), 
            ' del ', YEAR(reports.Sell_Date)) AS Fecha 
        FROM reports 
        INNER JOIN users ON reports.idSeller = users.idUser 
        INNER JOIN products ON reports.idProduct = products.idProduct 
        INNER JOIN routes ON reports.idRoute = routes.idRoute 
        INNER JOIN productssell ON products.idProduct = productssell.idProduct 
        INNER JOIN sells ON productssell.idSell = sells.idSell 
        INNER JOIN customers ON sells.idCustomer = customers.idCustomer 
        WHERE routes.idRoute = _idRoute;
    ELSEIF(_idRoute = 0) THEN
        SELECT customers.Name AS Cliente, CONCAT(users.Name, ' ', users.LastName) AS Vendedor, products.Name AS Producto, 
            routes.Name AS Ruta, productssell.Amount AS Cantidad, productssell.Total AS Total, 
            CONCAT(
                (SELECT
                CASE DAYNAME(reports.Sell_Date) WHEN 'Sunday' THEN 'Domingo' 
                WHEN 'Monday' THEN 'Lunes' 
                WHEN 'Tuesday' THEN 'Martes' 
                WHEN 'Wednesday' THEN 'Miercoles' 
                WHEN 'Thursday' THEN 'Jueves' 
                WHEN 'Friday' THEN 'Viernes' 
                WHEN 'Saturday' THEN 'Sabado' 
                END), 
                ' ', DAY(reports.Sell_Date), ' ', 
                (SELECT 
                CASE MONTH(reports.Sell_Date) WHEN 1 THEN 'Enero' 
                WHEN 2 THEN 'Febrero' 
                WHEN 3 THEN 'Marzo' 
                WHEN 4 THEN 'Abril' 
                WHEN 5 THEN 'Mayo' 
                WHEN 6 THEN 'Junio' 
                WHEN 7 THEN 'Julio' 
                WHEN 8 THEN 'Agosto' 
                WHEN 9 THEN 'Septiembre' 
                WHEN 10 THEN 'Octubre' 
                WHEN 11 THEN 'Noviembre' 
                WHEN 12 THEN 'Diciembre' 
                END), 
            ' del ', YEAR(reports.Sell_Date)) AS Fecha 
        FROM reports 
        INNER JOIN users ON reports.idSeller = users.idUser 
        INNER JOIN products ON reports.idProduct = products.idProduct 
        INNER JOIN routes ON reports.idRoute = routes.idRoute 
        INNER JOIN productssell ON products.idProduct = productssell.idProduct 
        INNER JOIN sells ON productssell.idSell = sells.idSell 
        INNER JOIN customers ON sells.idCustomer = customers.idCustomer;
    END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `reportsBySeller`(
_idSeller INT(11)
)
BEGIN
    IF(_idSeller > 0) THEN
        SELECT customers.Name AS Cliente, CONCAT(users.Name, ' ', users.LastName) AS Vendedor, products.Name AS Producto, 
            routes.Name AS Ruta, productssell.Amount AS Cantidad, productssell.Total AS Total, 
            CONCAT(
                (SELECT
                CASE DAYNAME(reports.Sell_Date) WHEN 'Sunday' THEN 'Domingo' 
                WHEN 'Monday' THEN 'Lunes' 
                WHEN 'Tuesday' THEN 'Martes' 
                WHEN 'Wednesday' THEN 'Miercoles' 
                WHEN 'Thursday' THEN 'Jueves' 
                WHEN 'Friday' THEN 'Viernes' 
                WHEN 'Saturday' THEN 'Sabado' 
                END), 
                ' ', DAY(reports.Sell_Date), ' ', 
                (SELECT 
                CASE MONTH(reports.Sell_Date) WHEN 1 THEN 'Enero' 
                WHEN 2 THEN 'Febrero' 
                WHEN 3 THEN 'Marzo' 
                WHEN 4 THEN 'Abril' 
                WHEN 5 THEN 'Mayo' 
                WHEN 6 THEN 'Junio' 
                WHEN 7 THEN 'Julio' 
                WHEN 8 THEN 'Agosto' 
                WHEN 9 THEN 'Septiembre' 
                WHEN 10 THEN 'Octubre' 
                WHEN 11 THEN 'Noviembre' 
                WHEN 12 THEN 'Diciembre' 
                END), 
            ' del ', YEAR(reports.Sell_Date)) AS Fecha 
        FROM reports 
        INNER JOIN users ON reports.idSeller = users.idUser 
        INNER JOIN products ON reports.idProduct = products.idProduct 
        INNER JOIN routes ON reports.idRoute = routes.idRoute 
        INNER JOIN productssell ON products.idProduct = productssell.idProduct 
        INNER JOIN sells ON productssell.idSell = sells.idSell 
        INNER JOIN customers ON sells.idCustomer = customers.idCustomer 
        WHERE users.idUser = _idSeller;
    ELSEIF(_idSeller = 0) THEN
        SELECT customers.Name AS Cliente, CONCAT(users.Name, ' ', users.LastName) AS Vendedor, products.Name AS Producto, 
            routes.Name AS Ruta, productssell.Amount AS Cantidad, productssell.Total AS Total, 
            CONCAT(
                (SELECT
                CASE DAYNAME(reports.Sell_Date) WHEN 'Sunday' THEN 'Domingo' 
                WHEN 'Monday' THEN 'Lunes' 
                WHEN 'Tuesday' THEN 'Martes' 
                WHEN 'Wednesday' THEN 'Miercoles' 
                WHEN 'Thursday' THEN 'Jueves' 
                WHEN 'Friday' THEN 'Viernes' 
                WHEN 'Saturday' THEN 'Sabado' 
                END), 
                ' ', DAY(reports.Sell_Date), ' ', 
                (SELECT 
                CASE MONTH(reports.Sell_Date) WHEN 1 THEN 'Enero' 
                WHEN 2 THEN 'Febrero' 
                WHEN 3 THEN 'Marzo' 
                WHEN 4 THEN 'Abril' 
                WHEN 5 THEN 'Mayo' 
                WHEN 6 THEN 'Junio' 
                WHEN 7 THEN 'Julio' 
                WHEN 8 THEN 'Agosto' 
                WHEN 9 THEN 'Septiembre' 
                WHEN 10 THEN 'Octubre' 
                WHEN 11 THEN 'Noviembre' 
                WHEN 12 THEN 'Diciembre' 
                END), 
            ' del ', YEAR(reports.Sell_Date)) AS Fecha 
        FROM reports 
        INNER JOIN users ON reports.idSeller = users.idUser 
        INNER JOIN products ON reports.idProduct = products.idProduct 
        INNER JOIN routes ON reports.idRoute = routes.idRoute 
        INNER JOIN productssell ON products.idProduct = productssell.idProduct 
        INNER JOIN sells ON productssell.idSell = sells.idSell 
        INNER JOIN customers ON sells.idCustomer = customers.idCustomer;
    END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `resetAssignedProducts`(
_idSeller INT(11)
)
BEGIN
    DELETE FROM productsseller WHERE idSeller = _idSeller;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `setAddressesCustomer`(
_idCustomer INT(11),
_Address VARCHAR(255),
_Phone VARCHAR(30)
)
BEGIN
    DECLARE _Last_ID int(11);
    
    IF(EXISTS(SELECT idAddressesCustomer FROM addressescustomer WHERE Address = _Address)) THEN
        SELECT TRUE AS Address_Exists;
    ELSE
        INSERT INTO addressescustomer (idCustomer, Address, Phone) VALUES (_idCustomer, _Address, _Phone);
        SET _Last_ID = LAST_INSERT_ID();
        SELECT _Last_ID AS idAddressesCustomer;        
    END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `setAssignedRoute`(
_idRoute INT(11),
_idSeller INT(11)
)
BEGIN
	IF(EXISTS(SELECT idSellerRoute FROM sellerroutes WHERE (idRoute = _idRoute) OR (idSeller = _idSeller))) THEN
		DELETE FROM sellerroutes WHERE (idRoute = _idRoute) OR (idSeller = _idSeller);
		INSERT INTO sellerroutes (idSeller, idRoute) VALUES(_idSeller, _idRoute);
	ELSE
		INSERT INTO sellerroutes (idSeller, idRoute) VALUES(_idSeller, _idRoute);
	END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `setCustomer`(
_Name VARCHAR(45),
_LastName VARCHAR(45),
_Email VARCHAR(45),
_RFC VARCHAR(30)
)
BEGIN
    DECLARE _Last_ID INT(11);
    
    IF(EXISTS(SELECT idCustomer FROM customers WHERE Email = _Email)) THEN
        SELECT TRUE AS Email_Exists;
    ELSEIF(EXISTS(SELECT idCustomer FROM customers WHERE RFC = _RFC)) THEN
        SELECT TRUE AS RFC_Exists;
    ELSE
        INSERT INTO customers (Name, LastName, Email, RFC) VALUES (_Name, _LastName, _Email, _RFC);
        SET _Last_ID = LAST_INSERT_ID();
        SELECT _Last_ID AS idCustomer;        
    END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `setCustomersRoutes`(
_idRoute INT(11),
_idAddressesCustomer INT(11)
)
BEGIN
    INSERT INTO customersroutes (idRoute, idAddressesCustomer) VALUES (_idRoute, _idAddressesCustomer);
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `setDataReport`(
_idRoute INT(11),
_idSeller INT(11),
_Sell_Date DATE,
_idProduct INT(11),
_Total FLOAT
)
BEGIN
    INSERT INTO reports(idRoute, idProduct, idSeller, Sell_Date, Total) VALUES(_idRoute, _idProduct, _idSeller, _Sell_Date, _Total);
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `setExpiredProducts`()
BEGIN
    DECLARE _Rows INT(11) DEFAULT 0;
    DECLARE _idProduct INT(11);
    DECLARE _Name VARCHAR(45);
    DECLARE _Amount INT(11);
    DECLARE _Price FLOAT;
    DECLARE _Description TEXT;
    DECLARE _Expiration_Date DATE;
    DECLARE _Action INT DEFAULT 0;
    DECLARE _Products CURSOR FOR SELECT idProduct, Name, Amount, Price, Description, Expiration_Date FROM products WHERE Expiration_Date < CURDATE();
    DECLARE CONTINUE HANDLER FOR NOT FOUND SET _Action = 1; 
    
    OPEN _Products;
    read_loop: LOOP
        FETCH _Products INTO _idProduct, _Name, _Amount, _Price, _Description, _Expiration_Date;
        IF(_Action = 1) THEN
            LEAVE read_loop;
        END IF;
        INSERT INTO expiredproducts(Name, Amount, Price, Description, Expiration_Date) VALUES (_Name, _Amount, _Price, _Description, _Expiration_Date);
        DELETE FROM products WHERE idProduct = _idProduct;
        SET _Rows = _Rows + 1;
    END LOOP;
    
    SELECT _Rows AS Rows_Affected;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `setProduct`(
_idPresentation INT(11),
_Name VARCHAR(45),
_Amount INT(11),
_Price FLOAT,
_Description TEXT,
_First_Date DATETIME,
_Expiration_Date DATE
)
BEGIN
    DECLARE _Last_ID INT(11);
    
    IF(EXISTS(SELECT idProduct FROM products WHERE (Name = _Name) AND (idPresentation = _idPresentation) AND (Expiration_Date = _Expiration_Date))) THEN
        SELECT TRUE AS Product_Exists;
    ELSE
        INSERT INTO products (idPresentation, Name, Amount, Price, Description, First_Date, Expiration_Date) VALUES (_idPresentation, _Name, _Amount, _Price, _Description, _First_Date, _Expiration_Date);
        SET _Last_ID = LAST_INSERT_ID();
        SELECT _Last_ID AS idProduct;        
    END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `setProductsSell`(
_idSell INT(11),
_idProduct INT(11),
_Amount INT(11)
)
BEGIN
    DECLARE _Total FLOAT;    
    SET _Total = (SELECT Price FROM products WHERE idProduct = _idProduct) * _Amount;
    
    INSERT INTO productssell(idSell, idProduct, Amount, Total) VALUES (_idSell, _idProduct, _Amount, _Total);
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `setRoute`(
_Name VARCHAR(45))
BEGIN
    DECLARE _Last_ID int(11);
    
    IF(EXISTS(SELECT idRoute FROM routes WHERE Name = _Name)) THEN
        SELECT TRUE AS Route_Exists;
    ELSE
        INSERT INTO routes(Name) VALUES(_Name);
        SET _Last_ID = LAST_INSERT_ID();
        SELECT _Last_ID AS idRoute;
    END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `setSell`(
_idCustomer int(11),
_idSeller int(11),
_Amount int(11),
_Total float,
_StartDate DATETIME
)
BEGIN
    DECLARE _Last_ID int(11);
    
    INSERT INTO sells(idCustomer, idSeller, Amount, Total, Start_Date) VALUES(_idCustomer, _idSeller, _Amount, _Total, _StartDate);
    SET _Last_ID = LAST_INSERT_ID();
    SELECT _Last_ID AS idSell;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `setUser`(
_idPrivilege INT(11),
_Name VARCHAR(45),
_LastName VARCHAR(45),
_Username VARCHAR(45),
_Password VARCHAR (45),
_Email VARCHAR(45),
_Phone VARCHAR(45),
_Address VARCHAR(255),
_RFC VARCHAR(30)
)
BEGIN
    DECLARE _Last_ID INT(11);
    
    IF(EXISTS(SELECT idUser FROM users WHERE Username = _Username)) THEN
        SELECT TRUE AS User_Exists;     
    ELSEIF(EXISTS(SELECT idUser FROM users WHERE Email = _Email)) THEN
        SELECT TRUE AS Email_Exists;
    ELSEIF(EXISTS(SELECT idUser FROM users WHERE RFC = _RFC)) THEN
        SELECT TRUE AS RFC_Exists;
    ELSE
        INSERT INTO users (idPrivilege, Name, LastName, Username, Password, Email, Phone, Address, RFC) VALUES (_idPrivilege, _Name, _LastName, _Username, _Password, _Email, _Phone, _Address, _RFC);
        SET _Last_ID = LAST_INSERT_ID();
        SELECT _Last_ID AS idUser;        
    END IF;
        
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `updateCustomer`(
_idCustomer INT(11),
_Name VARCHAR(45),
_LastName VARCHAR(45),
_Email VARCHAR(45),
_RFC VARCHAR(30))
BEGIN
    IF(EXISTS(SELECT idCustomer FROM customers WHERE idCustomer = _idCustomer)) THEN
        IF(EXISTS(SELECT idCustomer FROM customers WHERE (Email = _Email) AND (idCustomer <> _idCustomer))) THEN
            SELECT TRUE AS Email_Exists;
        ELSEIF(EXISTS(SELECT idCustomer FROM customers WHERE (RFC = _RFC) AND (idCustomer <> _idCustomer))) THEN
            SELECT TRUE AS RFC_Exists;
        ELSE
            UPDATE customers SET Name = _Name, LastName = _LastName, Email = _Email, RFC = _RFC WHERE idCustomer = _idCustomer;
            DELETE FROM addressescustomer WHERE idCustomer = _idCustomer;
            SELECT _idCustomer AS idCustomer;
        END IF;
    ELSE
        SELECT TRUE AS Customer_Not_Exists;        
    END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `updateProduct`(
_idProduct INT(11),
_idPresentation INT(11),
_Name VARCHAR(45),
_Amount INT(11),
_Price FLOAT,
_Description TEXT,
_Expiration_Date DATE
)
BEGIN
    IF(EXISTS(SELECT idProduct FROM products WHERE idProduct = _idProduct)) THEN
        IF(EXISTS(SELECT idProduct FROM products WHERE (Name = _Name) AND (idPresentation = _idPresentation) AND (Expiration_Date = _Expiration_Date) AND idProduct <> _idProduct)) THEN
            SELECT TRUE AS Product_Exists;
        ELSE
            UPDATE products SET idPresentation = _idPresentation, Name = _Name, Amount = _Amount, Price = _Price, Description = _Description, Expiration_Date = _Expiration_Date WHERE idProduct = _idProduct;
            
            SELECT _idProduct AS idProduct;
        END IF;
    ELSE
        SELECT TRUE AS Product_Not_Exists;        
    END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `updateProductsFromSeller`(
_idProduct INT(11),
_Amount INT(11))
BEGIN
    DECLARE newAmount INT(11);
    
    IF(EXISTS(SELECT idProduct FROM products WHERE idProduct = _idProduct)) THEN
        SET newAmount = (SELECT Amount FROM products WHERE idProduct = _idProduct) + _Amount;
        UPDATE products SET Amount = newAmount WHERE idProduct = _idProduct;
    ELSE
        SELECT TRUE AS Product_Not_Exist;
    END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `updateRoute`(
_idRoute INT(11),
_Name VARCHAR(45)
)
BEGIN
    IF(EXISTS(SELECT idRoute FROM routes WHERE idRoute = _idRoute)) THEN
        IF(EXISTS(SELECT idRoute FROM routes WHERE (Name = _Name) AND (idRoute <> _idRoute))) THEN
            SELECT TRUE AS Route_Exists;
        ELSE
            UPDATE routes SET Name = _Name WHERE idRoute = _idRoute;
            DELETE FROM customersroutes WHERE idRoute = _idRoute;
            SELECT _idRoute AS idRoute;
        END IF;
    ELSE
        SELECT TRUE AS Route_Not_Exists;        
    END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `updateUser`(
_idUser INT(11),
_idPrivilege INT(11),
_Name VARCHAR(45),
_LastName VARCHAR(45),
_Username VARCHAR(45),
_Password VARCHAR (45),
_Email VARCHAR(45),
_Phone VARCHAR(45),
_Address VARCHAR(255),
_RFC VARCHAR(30))
BEGIN    
    IF(EXISTS(SELECT idUser FROM users WHERE idUser = _idUser)) THEN
        IF(EXISTS(SELECT idUser FROM users WHERE (Username = _Username) AND (idUser <> _idUser))) THEN
            SELECT TRUE AS User_Exists;     
        ELSEIF(EXISTS(SELECT idUser FROM users WHERE (Email = _Email) AND (idUser <> _idUser))) THEN
            SELECT TRUE AS Email_Exists;
        ELSEIF(EXISTS(SELECT idUser FROM users WHERE (RFC = _RFC) AND (idUser <> _idUser))) THEN
            SELECT TRUE AS RFC_Exists;
        ELSE
            IF(_Password <> "") THEN
                UPDATE users SET idPrivilege = _idPrivilege, Name = _Name, LastName = _LastName, Username = _Username, Email = _Email, Phone = _Phone, Address = _Address, RFC = _RFC WHERE idUser = _idUser;
            ELSE
                UPDATE users SET idPrivilege = _idPrivilege, Name = _Name, LastName = _LastName, Username = _Username, Password = _Password, Email = _Email, Phone = _Phone, Address = _Address, RFC = _RFC WHERE idUser = _idUser;
            END IF;
            
            SELECT _idUser AS idUser;
        END IF;
    ELSE
        SELECT TRUE AS User_Not_Exists;        
    END IF;
END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `addressescustomer`
--

CREATE TABLE IF NOT EXISTS `addressescustomer` (
  `idAddressesCustomer` int(11) NOT NULL AUTO_INCREMENT,
  `idCustomer` int(11) NOT NULL,
  `Address` varchar(255) CHARACTER SET utf8 DEFAULT NULL,
  `Phone` varchar(30) CHARACTER SET utf8 DEFAULT NULL,
  PRIMARY KEY (`idAddressesCustomer`),
  KEY `fk_AddressesCustomer_Customers1` (`idCustomer`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=4 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `customers`
--

CREATE TABLE IF NOT EXISTS `customers` (
  `idCustomer` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) CHARACTER SET utf8 DEFAULT NULL,
  `LastName` varchar(45) CHARACTER SET utf8 DEFAULT NULL,
  `Email` varchar(45) CHARACTER SET utf8 DEFAULT NULL,
  `RFC` varchar(30) CHARACTER SET utf8 DEFAULT NULL,
  PRIMARY KEY (`idCustomer`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=4 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `customersroutes`
--

CREATE TABLE IF NOT EXISTS `customersroutes` (
  `idCustomersRoute` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `idRoute` int(11) NOT NULL,
  `idAddressesCustomer` int(11) NOT NULL,
  `Estimated_Time` date DEFAULT NULL,
  PRIMARY KEY (`idCustomersRoute`),
  KEY `fk_CustomersRoutes_Routes1` (`idRoute`),
  KEY `fk_CustomersRoutes_AddressesCustomer1` (`idAddressesCustomer`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `expiredproducts`
--

CREATE TABLE IF NOT EXISTS `expiredproducts` (
  `idExpiredProduct` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) CHARACTER SET utf8 DEFAULT NULL,
  `Amount` int(11) DEFAULT NULL,
  `Price` float DEFAULT NULL,
  `Description` text CHARACTER SET utf8,
  `Expiration_Date` date DEFAULT NULL,
  PRIMARY KEY (`idExpiredProduct`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `phonescustomer`
--

CREATE TABLE IF NOT EXISTS `phonescustomer` (
  `idPhonesCustomer` int(11) NOT NULL AUTO_INCREMENT,
  `idCustomer` int(11) NOT NULL,
  `Phone` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`idPhonesCustomer`),
  KEY `fk_PhonesCustomer_Customers1` (`idCustomer`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `presentations`
--

CREATE TABLE IF NOT EXISTS `presentations` (
  `idPresentation` int(11) NOT NULL AUTO_INCREMENT,
  `Presentation` varchar(45) CHARACTER SET utf8 DEFAULT NULL,
  PRIMARY KEY (`idPresentation`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=4 ;

--
-- Volcado de datos para la tabla `presentations`
--

INSERT INTO `presentations` (`idPresentation`, `Presentation`) VALUES
(1, '80 Gramos'),
(2, '150 Gramos'),
(3, '250 Gramos');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `privileges`
--

CREATE TABLE IF NOT EXISTS `privileges` (
  `idPrivilege` int(11) NOT NULL AUTO_INCREMENT,
  `Privilege` varchar(45) CHARACTER SET utf8 DEFAULT NULL,
  PRIMARY KEY (`idPrivilege`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=5 ;

--
-- Volcado de datos para la tabla `privileges`
--

INSERT INTO `privileges` (`idPrivilege`, `Privilege`) VALUES
(1, 'Gerente'),
(2, 'Jefe'),
(3, 'Inventario'),
(4, 'Vendedor');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `products`
--

CREATE TABLE IF NOT EXISTS `products` (
  `idProduct` int(11) NOT NULL AUTO_INCREMENT,
  `idPresentation` int(11) NOT NULL,
  `Name` varchar(45) CHARACTER SET utf8 DEFAULT NULL,
  `Amount` int(11) DEFAULT NULL,
  `Price` float DEFAULT NULL,
  `Description` text CHARACTER SET utf8,
  `First_Date` datetime DEFAULT NULL,
  `Expiration_Date` date DEFAULT NULL,
  PRIMARY KEY (`idProduct`),
  KEY `fk_Products_Presentations1` (`idPresentation`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=3 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `productssell`
--

CREATE TABLE IF NOT EXISTS `productssell` (
  `idProductsSell` int(11) NOT NULL AUTO_INCREMENT,
  `idSell` int(11) NOT NULL,
  `idProduct` int(11) NOT NULL,
  `Amount` int(11) DEFAULT NULL,
  `Total` float DEFAULT NULL,
  PRIMARY KEY (`idProductsSell`),
  KEY `fk_ProductsSell_Sells1` (`idSell`),
  KEY `fk_ProductsSell_Products1` (`idProduct`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=7 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `productsseller`
--

CREATE TABLE IF NOT EXISTS `productsseller` (
  `idProductsSeller` int(11) NOT NULL AUTO_INCREMENT,
  `idSeller` int(11) NOT NULL,
  `idProduct` int(11) NOT NULL,
  `Amount` int(11) DEFAULT NULL,
  PRIMARY KEY (`idProductsSeller`),
  KEY `fk_ProductsSeller_Users1` (`idSeller`),
  KEY `fk_ProductsSeller_Products1` (`idProduct`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=28 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `reports`
--

CREATE TABLE IF NOT EXISTS `reports` (
  `idReports` int(11) NOT NULL AUTO_INCREMENT,
  `idRoute` int(11) NOT NULL,
  `idProduct` int(11) NOT NULL,
  `idSeller` int(11) NOT NULL,
  `Sell_Date` date DEFAULT NULL,
  `Total` float DEFAULT NULL,
  PRIMARY KEY (`idReports`),
  KEY `fk_Reports_Routes1` (`idRoute`),
  KEY `fk_Reports_Products1` (`idProduct`),
  KEY `fk_Reports_Users1` (`idSeller`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=7 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `routes`
--

CREATE TABLE IF NOT EXISTS `routes` (
  `idRoute` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) CHARACTER SET utf8 DEFAULT NULL,
  PRIMARY KEY (`idRoute`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `sellerroutes`
--

CREATE TABLE IF NOT EXISTS `sellerroutes` (
  `idSellerRoute` int(11) NOT NULL AUTO_INCREMENT,
  `idSeller` int(11) NOT NULL,
  `idRoute` int(11) NOT NULL,
  PRIMARY KEY (`idSellerRoute`),
  KEY `fk_SellerRoutes_Users1` (`idSeller`),
  KEY `fk_SellerRoutes_Routes1` (`idRoute`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `sells`
--

CREATE TABLE IF NOT EXISTS `sells` (
  `idSell` int(11) NOT NULL AUTO_INCREMENT,
  `idCustomer` int(11) NOT NULL,
  `idSeller` int(11) NOT NULL,
  `Amount` int(11) DEFAULT NULL,
  `Total` float DEFAULT NULL,
  `Start_Date` date DEFAULT NULL,
  PRIMARY KEY (`idSell`),
  KEY `fk_Sells_Customers1` (`idCustomer`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=6 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `users`
--

CREATE TABLE IF NOT EXISTS `users` (
  `idUser` int(11) NOT NULL AUTO_INCREMENT,
  `idPrivilege` int(11) NOT NULL,
  `Name` varchar(45) CHARACTER SET utf8 DEFAULT NULL,
  `LastName` varchar(45) CHARACTER SET utf8 DEFAULT NULL,
  `Username` varchar(45) CHARACTER SET utf8 DEFAULT NULL,
  `Password` varchar(45) CHARACTER SET utf8 DEFAULT NULL,
  `Email` varchar(45) CHARACTER SET utf8 DEFAULT NULL,
  `Phone` varchar(45) CHARACTER SET utf8 DEFAULT NULL,
  `Address` varchar(255) CHARACTER SET utf8 DEFAULT NULL,
  `RFC` varchar(30) CHARACTER SET utf8 DEFAULT NULL,
  PRIMARY KEY (`idUser`),
  KEY `fk_Users_Privileges` (`idPrivilege`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=3 ;

--
-- Volcado de datos para la tabla `users`
--

INSERT INTO `users` (`idUser`, `idPrivilege`, `Name`, `LastName`, `Username`, `Password`, `Email`, `Phone`, `Address`, `RFC`) VALUES
(1, 1, 'Eduardo', 'Figarola', 'Diabulux', '95351130642aa8b849e6611acdc25384f6eca4c1', 'lalo.diabulux@gmail.com', '3121201309', 'Priv. Girasoles', 'FIME910911'),
(2, 4, 'José', 'Silverio', 'Vendedor', '95351130642aa8b849e6611acdc25384f6eca4c1', 'vendedor@gmail.com', '3121162037', 'Armeria #605', 'SIJO910911');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `visits`
--

CREATE TABLE IF NOT EXISTS `visits` (
  `idVisit` int(11) NOT NULL AUTO_INCREMENT,
  `idCustomer` int(11) NOT NULL,
  `Next_Date` date DEFAULT NULL,
  PRIMARY KEY (`idVisit`),
  KEY `fk_Visits_Customers1` (`idCustomer`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `addressescustomer`
--
ALTER TABLE `addressescustomer`
  ADD CONSTRAINT `addressescustomer_ibfk_1` FOREIGN KEY (`idCustomer`) REFERENCES `customers` (`idCustomer`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `customersroutes`
--
ALTER TABLE `customersroutes`
  ADD CONSTRAINT `customersroutes_ibfk_1` FOREIGN KEY (`idRoute`) REFERENCES `routes` (`idRoute`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `customersroutes_ibfk_2` FOREIGN KEY (`idAddressesCustomer`) REFERENCES `addressescustomer` (`idAddressesCustomer`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `phonescustomer`
--
ALTER TABLE `phonescustomer`
  ADD CONSTRAINT `fk_PhonesCustomer_Customers1` FOREIGN KEY (`idCustomer`) REFERENCES `customers` (`idCustomer`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Filtros para la tabla `products`
--
ALTER TABLE `products`
  ADD CONSTRAINT `products_ibfk_1` FOREIGN KEY (`idPresentation`) REFERENCES `presentations` (`idPresentation`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `productssell`
--
ALTER TABLE `productssell`
  ADD CONSTRAINT `productssell_ibfk_1` FOREIGN KEY (`idSell`) REFERENCES `sells` (`idSell`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `productssell_ibfk_2` FOREIGN KEY (`idProduct`) REFERENCES `products` (`idProduct`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `productsseller`
--
ALTER TABLE `productsseller`
  ADD CONSTRAINT `productsseller_ibfk_1` FOREIGN KEY (`idSeller`) REFERENCES `users` (`idUser`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `productsseller_ibfk_2` FOREIGN KEY (`idProduct`) REFERENCES `products` (`idProduct`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `reports`
--
ALTER TABLE `reports`
  ADD CONSTRAINT `reports_ibfk_1` FOREIGN KEY (`idRoute`) REFERENCES `routes` (`idRoute`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `reports_ibfk_2` FOREIGN KEY (`idProduct`) REFERENCES `products` (`idProduct`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `reports_ibfk_3` FOREIGN KEY (`idSeller`) REFERENCES `users` (`idUser`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `sellerroutes`
--
ALTER TABLE `sellerroutes`
  ADD CONSTRAINT `sellerroutes_ibfk_1` FOREIGN KEY (`idSeller`) REFERENCES `users` (`idUser`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `sellerroutes_ibfk_2` FOREIGN KEY (`idRoute`) REFERENCES `routes` (`idRoute`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `sells`
--
ALTER TABLE `sells`
  ADD CONSTRAINT `sells_ibfk_1` FOREIGN KEY (`idCustomer`) REFERENCES `customers` (`idCustomer`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `users`
--
ALTER TABLE `users`
  ADD CONSTRAINT `users_ibfk_1` FOREIGN KEY (`idPrivilege`) REFERENCES `privileges` (`idPrivilege`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `visits`
--
ALTER TABLE `visits`
  ADD CONSTRAINT `fk_Visits_Customers1` FOREIGN KEY (`idCustomer`) REFERENCES `customers` (`idCustomer`) ON DELETE NO ACTION ON UPDATE NO ACTION;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
