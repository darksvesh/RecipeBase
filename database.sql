CREATE DATABASE  IF NOT EXISTS `mydb` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `mydb`;
-- MySQL dump 10.13  Distrib 5.6.13, for Win32 (x86)
--
-- Host: localhost    Database: mydb
-- ------------------------------------------------------
-- Server version	5.5.23

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `budjet`
--

DROP TABLE IF EXISTS `budjet`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `budjet` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) NOT NULL,
  `Price` int(11) NOT NULL,
  `Data` date NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `budjet`
--

LOCK TABLES `budjet` WRITE;
/*!40000 ALTER TABLE `budjet` DISABLE KEYS */;
INSERT INTO `budjet` VALUES (7,'Крупа гречневая Ядрица 1 сорт в Пятерочка',-21,'2014-05-06'),(8,'Хлопья овсяные Селянка в Пятерочка',-33,'2014-05-06'),(10,'Чай Принцесса Нури в Гурман',-100,'2014-05-06'),(11,'Фунчоза в Ключик',-250,'2014-05-06');
/*!40000 ALTER TABLE `budjet` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ingredients`
--

DROP TABLE IF EXISTS `ingredients`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ingredients` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) NOT NULL,
  `HowMuch` varchar(45) NOT NULL,
  `Recipe` int(10) unsigned NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `Rec_to_ing_idx` (`Recipe`),
  CONSTRAINT `Rec_to_ing` FOREIGN KEY (`Recipe`) REFERENCES `recipe` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=34 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ingredients`
--

LOCK TABLES `ingredients` WRITE;
/*!40000 ALTER TABLE `ingredients` DISABLE KEYS */;
INSERT INTO `ingredients` VALUES (5,'Фунчоза','100  грамм',3),(6,'Филе куриное','200  грамм',3),(7,'Морковь','150  грамм',3),(8,'Перец болгарский','150  грамм',3),(9,'Огурец','150  грамм',3),(10,'Лук','150  грамм',3),(11,'Масло растительное','Для жарки',3),(23,'Молоко','250 мл',5),(24,'Дрожжи сухие','10 г',5),(25,'Сахар','100 г',5),(26,'Сливочное масло','100 г',5),(27,'Яйцо','2',5),(28,'Мука','400-450 г',5),(29,'Соль','1/2 ч.л.',5),(30,'Творог','500 г',5),(31,'Сметана','100 г',5),(32,'Сахар','70-100 г',5),(33,'Сахар ванильный','2 ч.л.',5);
/*!40000 ALTER TABLE `ingredients` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `need`
--

DROP TABLE IF EXISTS `need`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `need` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `need`
--

LOCK TABLES `need` WRITE;
/*!40000 ALTER TABLE `need` DISABLE KEYS */;
/*!40000 ALTER TABLE `need` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `productbase`
--

DROP TABLE IF EXISTS `productbase`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `productbase` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Barcode` varchar(15) NOT NULL,
  `Name` varchar(45) NOT NULL,
  `ShelfLife` int(10) unsigned zerofill NOT NULL,
  `KCal` int(11) unsigned zerofill NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  UNIQUE KEY `Barcode_UNIQUE` (`Barcode`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `productbase`
--

LOCK TABLES `productbase` WRITE;
/*!40000 ALTER TABLE `productbase` DISABLE KEYS */;
INSERT INTO `productbase` VALUES (7,'4607084910216','Крупа гречневая Ядрица 1 сорт',0000000600,00000000335),(8,'4600819311708','Хлопья овсяные Селянка',0000000365,00000000250),(10,'4605246002014','Чай Принцесса Нури',0000001095,00000000000),(11,'4607078450230','Фунчоза',0000000365,00000000351);
/*!40000 ALTER TABLE `productbase` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `productfridge`
--

DROP TABLE IF EXISTS `productfridge`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `productfridge` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Barcode` varchar(15) NOT NULL,
  `BuyDate` date NOT NULL,
  `ShopId` int(11) NOT NULL,
  `Price` int(11) NOT NULL,
  `CreateDate` date NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `shop_to_shopid_idx` (`ShopId`),
  KEY `bar_to_bar_idx` (`Barcode`),
  CONSTRAINT `bar_to_bar` FOREIGN KEY (`Barcode`) REFERENCES `productbase` (`Barcode`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `shop_to_shopid` FOREIGN KEY (`ShopId`) REFERENCES `shoplist` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `productfridge`
--

LOCK TABLES `productfridge` WRITE;
/*!40000 ALTER TABLE `productfridge` DISABLE KEYS */;
INSERT INTO `productfridge` VALUES (10,'4607084910216','2014-05-06',5,21,'2014-05-06'),(11,'4600819311708','2014-05-06',5,33,'2014-05-06'),(13,'4605246002014','2014-05-06',6,100,'2014-05-06'),(14,'4607078450230','2014-05-06',7,250,'2013-07-03');
/*!40000 ALTER TABLE `productfridge` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `recipe`
--

DROP TABLE IF EXISTS `recipe`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `recipe` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) NOT NULL,
  `HowTo` longtext NOT NULL,
  `DishCategory` int(10) unsigned NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `recipe`
--

LOCK TABLES `recipe` WRITE;
/*!40000 ALTER TABLE `recipe` DISABLE KEYS */;
INSERT INTO `recipe` VALUES (3,'Фунчоза с овощами и куриным филе','Фунчоза - это вермишель из бобовой муки.\nКупить ее можно в отделах, где продается все для китайской кухни (в крупных супермаркетах или на рынке).\nЗаменить ее ничем нельзя.\nИз указанного количества ингредиентов получается 5-7 порций.\n\nЛук нарезать соломкой. Филе нарезать соломкой. Морковь нашинковать соломкой.Перец очистить от семян, нарезать соломкой. Огурцы \nнарезать тонкой соломкой. Зеленый лук мелко нарезать. На растительном масле обжарить лук. Добавить филе, жарить в течение 10 минут.\nДобавить морковь. Следом добавить перец, жарить 3-4 минуты. Фунчозу приготовить так, как написано на упаковке. Затем промыла в холодной воде.\nПо желанию, ее можно нарезать. Смешать фунчозу, огурцы, лук, филе с морковью и перцем.\nПосолить, поперчить. По желанию, добавить кориандр.\nЖелательно, дать салату настояться в течение часа.',0),(5,'Ватрушки с творогом','Вместо творога можно использовать повидло или джем.\nЕсли используете творог, в него можно добавить изюм, цукаты, орешки.\nИз указанного количества ингредиентов получается около 20 ватрушек.\n\n  Готовим тесто.\nМолоко подогреть (оно не должно быть сильно горячим).\nДобавить 1 ст.л. сахара и дрожжи. Добавить 150 г муки, перемешать.Поставить опару в теплое место (я наливаю в миску горячую воду, в нее ставлю емкость с опарой). Дать опаре хорошо подняться (это займет около 15 минут). Смешать опару и яйца. Добавить масло, перемешать. Добавить сахар и соль, перемешать. Добавить муку, замесить тесто.\nПоставить его в теплое место.\nДать тесту подняться (это займет около 30 минут). Тесто обмять, снова поставить в теплое место. Дать подняться.\n   Готовим начинку.\nТворог смешать с сахаром и ванильным сахаром. Добавить сметану, перемешать. Яйцо взболтать.\n\n   Противень смазать маслом или застелить бумагой для выпечки.\nИз теста сформировать лепешечки, выложить их на противень. Смазать яйцом. В каждой лепешечке сделать углубление. Выложить начинку.\nПоставить в разогретую до 180 градусов духовку.\nВыпекать в течение 20-25 минут.',4);
/*!40000 ALTER TABLE `recipe` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `recipepic`
--

DROP TABLE IF EXISTS `recipepic`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `recipepic` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Recipe` int(10) unsigned NOT NULL,
  `Path` varchar(45) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `Rec_to_pic_idx` (`Recipe`),
  CONSTRAINT `Rec_to_pic` FOREIGN KEY (`Recipe`) REFERENCES `recipe` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=35 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `recipepic`
--

LOCK TABLES `recipepic` WRITE;
/*!40000 ALTER TABLE `recipepic` DISABLE KEYS */;
INSERT INTO `recipepic` VALUES (3,3,'2014-05-06-12-05-25.jpg'),(4,3,'2014-05-06-12-05-33.jpg'),(5,3,'2014-05-06-12-05-36.jpg'),(6,3,'2014-05-06-12-05-40.jpg'),(7,3,'2014-05-06-12-05-43.jpg'),(8,3,'2014-05-06-12-05-48.jpg'),(9,3,'2014-05-06-12-05-52.jpg'),(10,3,'2014-05-06-12-05-59.jpg'),(11,3,'2014-05-06-12-05-05.jpg'),(12,3,'2014-05-06-12-05-10.jpg'),(13,3,'2014-05-06-12-05-18.jpg'),(25,5,'2014-05-06-12-40-35.jpg'),(26,5,'2014-05-06-12-40-40.jpg'),(27,5,'2014-05-06-12-40-43.jpg'),(28,5,'2014-05-06-12-40-49.jpg'),(29,5,'2014-05-06-12-40-56.jpg'),(30,5,'2014-05-06-12-41-04.jpg'),(31,5,'2014-05-06-12-41-12.jpg'),(32,5,'2014-05-06-12-41-17.jpg'),(33,5,'2014-05-06-12-41-22.jpg'),(34,5,'2014-05-06-12-41-26.jpg');
/*!40000 ALTER TABLE `recipepic` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `shoplist`
--

DROP TABLE IF EXISTS `shoplist`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `shoplist` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `idstore_UNIQUE` (`id`),
  UNIQUE KEY `Магаз_UNIQUE` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `shoplist`
--

LOCK TABLES `shoplist` WRITE;
/*!40000 ALTER TABLE `shoplist` DISABLE KEYS */;
INSERT INTO `shoplist` VALUES (6,'Гурман'),(7,'Ключик'),(8,'Магнит'),(5,'Пятерочка');
/*!40000 ALTER TABLE `shoplist` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2014-05-06 14:13:15
