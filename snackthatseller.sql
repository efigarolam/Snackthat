-- phpMyAdmin SQL Dump
-- version 3.4.5
-- http://www.phpmyadmin.net
--
-- Servidor: localhost
-- Tiempo de generación: 30-05-2012 a las 19:01:58
-- Versión del servidor: 5.5.16
-- Versión de PHP: 5.3.8

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Base de datos: `snackthatseller`
--

DELIMITER $$
--
-- Procedimientos
--
CREATE DEFINER=`root`@`localhost` PROCEDURE `cleanAll`()
BEGIN
    DELETE FROM routes;
    DELETE FROM customersroutes;
    DELETE FROM addressescustomer;
    DELETE FROM customers;
    DELETE FROM productssell;
    DELETE FROM products;
    DELETE FROM sells;    
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `deleteCustomerByID`(
_idCustomer INT(11)
)
BEGIN
    IF(EXISTS(SELECT idCustomer FROM customers WHERE idCustomer = _idCustomer)) THEN
        DELETE FROM customers WHERE idCustomer = _idCustomer;
    ELSE
        SELECT TRUE AS Customer_Not_Exists;
    END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `deleteProductsContent`()
BEGIN
    DELETE FROM products;
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `deleteUsers`()
BEGIN
    DELETE FROM users;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getAllCustomers`(
_data INT(11))
BEGIN
    IF(_data = 0) THEN
        SELECT idCustomer AS ID, CONCAT(Name, ' ',LastName) AS Nombre, Email AS Correo, RFC
        FROM customers;
    ELSEIF(_data = 1) THEN
        SELECT customers.idCustomer AS ID, customers.Name AS Nombre, customers.LastName AS Apellidos, customers.Email AS Correo, customers.RFC, Address AS Direccion, Phone AS Telefono
        FROM addressescustomer INNER JOIN customers ON addressescustomer.idCustomer = customers.idCustomer;
    END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getAllProducts`()
BEGIN
    SELECT products.idProduct AS ID, products.Name AS Nombre, presentations.Presentation AS Presentacion, products.Amount AS Cantidad, products.Price AS Precio
    FROM products INNER JOIN presentations ON products.idPresentation = presentations.idPresentation;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getAllProductsSell`()
BEGIN
    SELECT idSell, idProduct, Amount, Total FROM productssell;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getAllRoutes`()
BEGIN
    SELECT idRoute AS ID, Name AS Nombre
    FROM routes;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getAllSells`()
BEGIN
    SELECT idSell, idCustomer AS Cliente, idSeller AS Vendedor, Amount AS Cantidad, Total, Start_Date AS Fecha FROM sells;
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `getIDByUsername`(
_Username VARCHAR(45)
)
BEGIN
    IF(EXISTS(SELECT idUser FROM users WHERE Username = _Username)) THEN
        SELECT idUser FROM users WHERE Username = _Username;
    ELSE
        SELECT 0 AS idUser;
    END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getLastCustomers`()
BEGIN
    SELECT idCustomer AS ID, CONCAT(Name, ' ', LastName) AS Nombre, Email AS Correo, RFC
    FROM customers ORDER BY idCustomer DESC LIMIT 5;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getLastSells`()
BEGIN
    SELECT 
        CONCAT(customers.Name, ' ', customers.LastName) AS Cliente,
        sells.Amount AS Cantidad,
        sells.Total
    FROM
        sells
            INNER JOIN
        customers ON sells.idCustomer = customers.idCustomer
    ORDER BY idSell DESC LIMIT 5;
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
        SELECT products.idProduct AS ID, products.Name AS Nombre, products.Description AS Descripcion, presentations.Presentation AS Presentacion, products.Amount AS Cantidad, products.Price AS Precio
        FROM products INNER JOIN presentations ON products.idPresentation = presentations.idPresentation
        WHERE (products.idProduct = _idProduct);
    ELSE
        SELECT TRUE AS Product_Not_Exists;
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `getSellByID`(
_idSell INT(11)
)
BEGIN
    IF(EXISTS(SELECT idSell FROM sells WHERE idSell = _idSell)) THEN
        SELECT 
            sells.idSell AS ID,
            CONCAT(customers.Name, ' ', customers.LastName) AS Cliente,
            sells.Amount AS Cantidad,
            sells.Total,
            sells.Start_Date AS Fecha
        FROM
            sells
                INNER JOIN
            customers ON sells.idCustomer = customers.idCustomer
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
        sells.Amount AS Cantidad,
        sells.Total,
        sells.Start_Date AS Fecha
    FROM
        sells
            INNER JOIN
        customers ON sells.idCustomer = customers.idCustomer;
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `login`(
_Username VARCHAR(45),
_Password VARCHAR(45)
)
BEGIN
	SELECT idUser FROM users WHERE (Username = _Username) AND (Password = _Password);
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `setAddressesCustomer`(
_idAddressesCustomer INT(11),
_idCustomer INT(11),
_Address VARCHAR(255),
_Phone VARCHAR(30)
)
BEGIN
	INSERT INTO addressescustomer (idAddressesCustomer, idCustomer, Address, Phone) VALUES (_idAddressesCustomer, _idCustomer, _Address, _Phone);
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
_idCustomer INT(11),
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
        INSERT INTO customers (idCustomer, Name, LastName, Email, RFC) VALUES (_idCustomer, _Name, _LastName, _Email, _RFC);
        SET _Last_ID = LAST_INSERT_ID();
        SELECT _Last_ID AS idCustomer;        
    END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `setCustomersRoutes`(
_idCustomersRoute INT(11),
_idRoute INT(11),
_idAddressesCustomer INT(11),
_EstimatedTime DATE
)
BEGIN
    IF(_EstimatedTime <> null) THEN
        INSERT INTO customersroutes (idCustomersRoute, idRoute, idAddressesCustomer, Estimated_Time) VALUES (_idCustomersRoute, _idRoute, _idAddressesCustomer, _EstimatedTime);        
    ELSE
        INSERT INTO customersroutes (idCustomersRoute, idRoute, idAddressesCustomer) VALUES (_idCustomersRoute, _idRoute, _idAddressesCustomer);
    END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `setProduct`(
_idProduct INT(11),
_idPresentation INT(11),
_Name VARCHAR(45),
_Amount INT(11),
_Price FLOAT,
_Description TEXT
)
BEGIN
    DECLARE _Last_ID INT(11);
    
    IF(EXISTS(SELECT idProduct FROM products WHERE (Name = _Name) AND (idPresentation = _idPresentation))) THEN
        SELECT TRUE AS Product_Exists;
    ELSE
        INSERT INTO products (idProduct, idPresentation, Name, Amount, Price, Description) VALUES (_idProduct, _idPresentation, _Name, _Amount, _Price, _Description);
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
    
    UPDATE products SET Amount = (Amount - _Amount) WHERE idProduct = _idProduct;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `setRoute`(
_idRoute INT(11),
_Name VARCHAR(45))
BEGIN    
    DELETE FROM routes;
    DELETE FROM customersroutes;
    DELETE FROM addressescustomer;
    DELETE FROM customers;
    
    INSERT INTO routes(idRoute, Name) VALUES(_idRoute, _Name);
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
_idUser INT(11),
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
        INSERT INTO users (idUser, idPrivilege, Name, LastName, Username, Password, Email, Phone, Address, RFC) VALUES (_idUser, _idPrivilege, _Name, _LastName, _Username, _Password, _Email, _Phone, _Address, _RFC);
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

DELIMITER ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `addressescustomer`
--

CREATE TABLE IF NOT EXISTS `addressescustomer` (
  `idAddressesCustomer` int(11) NOT NULL,
  `idCustomer` int(11) NOT NULL,
  `Address` varchar(255) CHARACTER SET utf8 DEFAULT NULL,
  `Phone` varchar(30) CHARACTER SET utf8 DEFAULT NULL,
  PRIMARY KEY (`idAddressesCustomer`),
  KEY `fk_AddressesCustomer_Customers1` (`idCustomer`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `customers`
--

CREATE TABLE IF NOT EXISTS `customers` (
  `idCustomer` int(11) NOT NULL,
  `Name` varchar(45) CHARACTER SET utf8 DEFAULT NULL,
  `LastName` varchar(45) CHARACTER SET utf8 DEFAULT NULL,
  `Email` varchar(45) CHARACTER SET utf8 DEFAULT NULL,
  `RFC` varchar(30) CHARACTER SET utf8 DEFAULT NULL,
  PRIMARY KEY (`idCustomer`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `customersroutes`
--

CREATE TABLE IF NOT EXISTS `customersroutes` (
  `idCustomersRoute` int(11) unsigned NOT NULL,
  `idRoute` int(11) NOT NULL,
  `idAddressesCustomer` int(11) NOT NULL,
  `Estimated_Time` date DEFAULT NULL,
  PRIMARY KEY (`idCustomersRoute`),
  KEY `fk_CustomersRoutes_Routes1` (`idRoute`),
  KEY `fk_CustomersRoutes_AddressesCustomer1` (`idAddressesCustomer`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `expiredproducts`
--

CREATE TABLE IF NOT EXISTS `expiredproducts` (
  `idExpiredProduct` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) CHARACTER SET utf8 DEFAULT NULL,
  `Amount` int(11) DEFAULT NULL,
  `Description` text CHARACTER SET utf8,
  `Expiration_Date` date DEFAULT NULL,
  `Price` float DEFAULT NULL,
  PRIMARY KEY (`idExpiredProduct`)
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
  `idProduct` int(11) NOT NULL,
  `idPresentation` int(11) NOT NULL,
  `Name` varchar(45) CHARACTER SET utf8 DEFAULT NULL,
  `Amount` int(11) DEFAULT NULL,
  `Price` float DEFAULT NULL,
  `Description` text CHARACTER SET utf8,
  PRIMARY KEY (`idProduct`),
  KEY `fk_Products_Presentations1` (`idPresentation`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `products`
--

INSERT INTO `products` (`idProduct`, `idPresentation`, `Name`, `Amount`, `Price`, `Description`) VALUES
(1, 1, 'Producto 1', 10, 20, 'Producto 1'),
(2, 1, 'Sabritas', 10, 5, 'Papas');

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
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `routes`
--

CREATE TABLE IF NOT EXISTS `routes` (
  `idRoute` int(11) NOT NULL,
  `Name` varchar(45) CHARACTER SET utf8 DEFAULT NULL,
  PRIMARY KEY (`idRoute`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `routes`
--

INSERT INTO `routes` (`idRoute`, `Name`) VALUES
(1, 'Valle de Las Garzas');

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
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `users`
--

CREATE TABLE IF NOT EXISTS `users` (
  `idUser` int(11) NOT NULL,
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
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `users`
--

INSERT INTO `users` (`idUser`, `idPrivilege`, `Name`, `LastName`, `Username`, `Password`, `Email`, `Phone`, `Address`, `RFC`) VALUES
(2, 4, 'José', 'Silverio', 'Vendedor', '95351130642aa8b849e6611acdc25384f6eca4c1', 'vendedor@gmail.com', '3121162037', 'Armeria #605', 'SIJO910911');

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
-- Filtros para la tabla `sells`
--
ALTER TABLE `sells`
  ADD CONSTRAINT `sells_ibfk_1` FOREIGN KEY (`idCustomer`) REFERENCES `customers` (`idCustomer`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `users`
--
ALTER TABLE `users`
  ADD CONSTRAINT `fk_Users_Privileges` FOREIGN KEY (`idPrivilege`) REFERENCES `privileges` (`idPrivilege`) ON DELETE NO ACTION ON UPDATE NO ACTION;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
