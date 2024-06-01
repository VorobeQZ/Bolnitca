CREATE DATABASE  IF NOT EXISTS `policlinica` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `policlinica`;
-- MySQL dump 10.13  Distrib 8.0.36, for Win64 (x86_64)
--
-- Host: localhost    Database: policlinica
-- ------------------------------------------------------
-- Server version	8.0.36

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `logins`
--

DROP TABLE IF EXISTS `logins`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `logins` (
  `Код` int NOT NULL,
  `Login` varchar(50) DEFAULT NULL,
  `Password` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Код`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `logins`
--

LOCK TABLES `logins` WRITE;
/*!40000 ALTER TABLE `logins` DISABLE KEYS */;
INSERT INTO `logins` VALUES (1,'admin','admin'),(2,'doctor','doctor');
/*!40000 ALTER TABLE `logins` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `анализ`
--

DROP TABLE IF EXISTS `анализ`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `анализ` (
  `Код` int NOT NULL,
  `Наименование` varchar(60) DEFAULT NULL,
  PRIMARY KEY (`Код`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `анализ`
--

LOCK TABLES `анализ` WRITE;
/*!40000 ALTER TABLE `анализ` DISABLE KEYS */;
INSERT INTO `анализ` VALUES (1,'Общий анализ крови'),(2,'Сдача крови на сахар');
/*!40000 ALTER TABLE `анализ` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `анализы`
--

DROP TABLE IF EXISTS `анализы`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `анализы` (
  `Код` int NOT NULL,
  `Пациент` int DEFAULT NULL,
  `Персонал` int DEFAULT NULL,
  `Анализ` int DEFAULT NULL,
  `Дата` varchar(60) DEFAULT NULL,
  `Результат` text,
  PRIMARY KEY (`Код`),
  KEY `анализы_анализ_Код_fk` (`Анализ`),
  KEY `анализы_пациент_Код_fk` (`Пациент`),
  KEY `анализы_персонал_Код_fk` (`Персонал`),
  CONSTRAINT `анализы_анализ_Код_fk` FOREIGN KEY (`Анализ`) REFERENCES `анализ` (`Код`),
  CONSTRAINT `анализы_пациент_Код_fk` FOREIGN KEY (`Пациент`) REFERENCES `пациент` (`Код`),
  CONSTRAINT `анализы_персонал_Код_fk` FOREIGN KEY (`Персонал`) REFERENCES `персонал` (`Код`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `анализы`
--

LOCK TABLES `анализы` WRITE;
/*!40000 ALTER TABLE `анализы` DISABLE KEYS */;
INSERT INTO `анализы` VALUES (1,1,1,1,'10.05.2024 0:00:00','Много букв и цифр'),(2,1,3,1,'','');
/*!40000 ALTER TABLE `анализы` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `должность`
--

DROP TABLE IF EXISTS `должность`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `должность` (
  `Код` int NOT NULL,
  `Наименование` varchar(60) DEFAULT NULL,
  PRIMARY KEY (`Код`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `должность`
--

LOCK TABLES `должность` WRITE;
/*!40000 ALTER TABLE `должность` DISABLE KEYS */;
INSERT INTO `должность` VALUES (1,'Терапевт'),(2,'Аллерголог'),(3,'Диетолог'),(4,'Стоматолог'),(5,'Хирург'),(6,'УЗИ-специалист');
/*!40000 ALTER TABLE `должность` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `кабинет`
--

DROP TABLE IF EXISTS `кабинет`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `кабинет` (
  `Код` int NOT NULL,
  `Наименование` varchar(60) DEFAULT NULL,
  `Телефон` varchar(60) DEFAULT NULL,
  PRIMARY KEY (`Код`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `кабинет`
--

LOCK TABLES `кабинет` WRITE;
/*!40000 ALTER TABLE `кабинет` DISABLE KEYS */;
INSERT INTO `кабинет` VALUES (1,'А303','303'),(2,'А304','304'),(3,'А305','305'),(4,'А306','306'),(5,'А307','307'),(6,'А308','308');
/*!40000 ALTER TABLE `кабинет` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `пациент`
--

DROP TABLE IF EXISTS `пациент`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `пациент` (
  `Код` int NOT NULL,
  `Фамилия` varchar(60) DEFAULT NULL,
  `Имя` varchar(60) DEFAULT NULL,
  `Отчество` varchar(60) DEFAULT NULL,
  `Пол` varchar(12) DEFAULT NULL,
  `Возраст` int DEFAULT NULL,
  `Телефон` varchar(12) DEFAULT NULL,
  `Полис` varchar(30) DEFAULT NULL,
  PRIMARY KEY (`Код`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `пациент`
--

LOCK TABLES `пациент` WRITE;
/*!40000 ALTER TABLE `пациент` DISABLE KEYS */;
INSERT INTO `пациент` VALUES (1,' Иванов','Иван','Иванович','Мужской',33,'456456','4564');
/*!40000 ALTER TABLE `пациент` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `персонал`
--

DROP TABLE IF EXISTS `персонал`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `персонал` (
  `Код` int NOT NULL,
  `Фамилия` varchar(60) DEFAULT NULL,
  `Имя` varchar(60) DEFAULT NULL,
  `Отчество` varchar(60) DEFAULT NULL,
  `Пол` varchar(12) DEFAULT NULL,
  `Кабинет` int DEFAULT NULL,
  `Должность` int DEFAULT NULL,
  PRIMARY KEY (`Код`),
  KEY `персонал_должность_Код_fk` (`Должность`),
  KEY `персонал_кабинет_Код_fk` (`Кабинет`),
  CONSTRAINT `персонал_должность_Код_fk` FOREIGN KEY (`Должность`) REFERENCES `должность` (`Код`),
  CONSTRAINT `персонал_кабинет_Код_fk` FOREIGN KEY (`Кабинет`) REFERENCES `кабинет` (`Код`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `персонал`
--

LOCK TABLES `персонал` WRITE;
/*!40000 ALTER TABLE `персонал` DISABLE KEYS */;
INSERT INTO `персонал` VALUES (1,'Антипова','Ксения','Михайлова','Женский',1,1),(2,'Астахов','Владимир','Алиевич','Мужской',2,2),(3,'Яковлева','Варвара','Никитична','Женский',3,3),(4,'Попова','Варвара','Андреевна','Женский',4,4),(5,'Терехов','Антон','Михайлович','Мужской',5,5),(6,'Иванов','Иван','Иванович','Мужской',4,3);
/*!40000 ALTER TABLE `персонал` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `расписание`
--

DROP TABLE IF EXISTS `расписание`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `расписание` (
  `Код` int NOT NULL,
  `Время` varchar(60) DEFAULT NULL,
  `Анализ` int DEFAULT NULL,
  `Персонал` int DEFAULT NULL,
  PRIMARY KEY (`Код`),
  KEY `расписание_анализ_Код_fk` (`Анализ`),
  KEY `расписание_персонал_Код_fk` (`Персонал`),
  CONSTRAINT `расписание_анализ_Код_fk` FOREIGN KEY (`Анализ`) REFERENCES `анализ` (`Код`),
  CONSTRAINT `расписание_персонал_Код_fk` FOREIGN KEY (`Персонал`) REFERENCES `персонал` (`Код`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `расписание`
--

LOCK TABLES `расписание` WRITE;
/*!40000 ALTER TABLE `расписание` DISABLE KEYS */;
INSERT INTO `расписание` VALUES (1,'07:15:00',1,1);
/*!40000 ALTER TABLE `расписание` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'policlinica'
--

--
-- Dumping routines for database 'policlinica'
--
/*!50003 DROP PROCEDURE IF EXISTS `Персонал` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `Персонал`()
BEGIN
select Персонал.Код, Фамилия, Имя, Отчество, Пол, 
Кабинет.Наименование as Кабинет ,Должность.Наименование as Должность
from персонал inner join кабинет on персонал.кабинет=кабинет.Код
inner join должность on персонал.должность=Должность.Код ;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Расписание` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `Расписание`()
BEGIN
select Время,анализ.наименование as Анализ,CONCAT(персонал.Фамилия,' ',персонал.Имя,' ',персонал.Отчество)as "Персонал",кабинет.наименование as Кабинет from расписание 
Join кабинет on расписание.персонал=кабинет.код
join анализ on расписание.анализ=анализ.код
join Персонал on расписание.персонал=персонал.код;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-06-01 23:28:53
