CREATE DATABASE  IF NOT EXISTS `pumaprey` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `pumaprey`;
-- MySQL dump 10.13  Distrib 8.0.27, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: pumaprey
-- ------------------------------------------------------
-- Server version	8.0.27

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
-- Table structure for table `Animals`
--

DROP TABLE IF EXISTS `Animals`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Animals` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `SafariId` int NOT NULL,
  `AnimalName` longtext,
  `Species` longtext,
  `Weight` longtext,
  `Color` longtext,
  `DateOfBirth` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Animals_SafariId` (`SafariId`),
  CONSTRAINT `FK_Animals_Safaris_SafariId` FOREIGN KEY (`SafariId`) REFERENCES `Safaris` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=1343 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Animals`
--

LOCK TABLES `Animals` WRITE;
/*!40000 ALTER TABLE `Animals` DISABLE KEYS */;
INSERT INTO `Animals` VALUES (1096,1997,'Jerry','Wolf','110lb','White','1994-02-13 00:00:00.000000'),(1234,1995,'Jeff','Tiger','250lb','yellow','1997-03-01 00:00:00.000000'),(1342,1996,'Duff','Lion','260lb','Grey','1999-04-13 00:00:00.000000');
/*!40000 ALTER TABLE `Animals` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `PumaRoleClaims`
--

DROP TABLE IF EXISTS `PumaRoleClaims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `PumaRoleClaims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `RoleId` varchar(255) NOT NULL,
  `ClaimType` longtext,
  `ClaimValue` longtext,
  PRIMARY KEY (`Id`),
  KEY `IX_PumaRoleClaims_RoleId` (`RoleId`),
  CONSTRAINT `FK_PumaRoleClaims_PumaRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `PumaRoles` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `PumaRoleClaims`
--

LOCK TABLES `PumaRoleClaims` WRITE;
/*!40000 ALTER TABLE `PumaRoleClaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `PumaRoleClaims` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `PumaRoles`
--

DROP TABLE IF EXISTS `PumaRoles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `PumaRoles` (
  `Id` varchar(255) NOT NULL,
  `Name` varchar(256) DEFAULT NULL,
  `NormalizedName` varchar(256) DEFAULT NULL,
  `ConcurrencyStamp` longtext,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `RoleNameIndex` (`NormalizedName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `PumaRoles`
--

LOCK TABLES `PumaRoles` WRITE;
/*!40000 ALTER TABLE `PumaRoles` DISABLE KEYS */;
/*!40000 ALTER TABLE `PumaRoles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `PumaUserClaims`
--

DROP TABLE IF EXISTS `PumaUserClaims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `PumaUserClaims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` varchar(255) NOT NULL,
  `ClaimType` longtext,
  `ClaimValue` longtext,
  PRIMARY KEY (`Id`),
  KEY `IX_PumaUserClaims_UserId` (`UserId`),
  CONSTRAINT `FK_PumaUserClaims_PumaUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `PumaUsers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `PumaUserClaims`
--

LOCK TABLES `PumaUserClaims` WRITE;
/*!40000 ALTER TABLE `PumaUserClaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `PumaUserClaims` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `PumaUserLogins`
--

DROP TABLE IF EXISTS `PumaUserLogins`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `PumaUserLogins` (
  `LoginProvider` varchar(255) NOT NULL,
  `ProviderKey` varchar(255) NOT NULL,
  `ProviderDisplayName` longtext,
  `UserId` varchar(255) NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`),
  KEY `IX_PumaUserLogins_UserId` (`UserId`),
  CONSTRAINT `FK_PumaUserLogins_PumaUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `PumaUsers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `PumaUserLogins`
--

LOCK TABLES `PumaUserLogins` WRITE;
/*!40000 ALTER TABLE `PumaUserLogins` DISABLE KEYS */;
/*!40000 ALTER TABLE `PumaUserLogins` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `PumaUserRoles`
--

DROP TABLE IF EXISTS `PumaUserRoles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `PumaUserRoles` (
  `UserId` varchar(255) NOT NULL,
  `RoleId` varchar(255) NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IX_PumaUserRoles_RoleId` (`RoleId`),
  CONSTRAINT `FK_PumaUserRoles_PumaRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `PumaRoles` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_PumaUserRoles_PumaUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `PumaUsers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `PumaUserRoles`
--

LOCK TABLES `PumaUserRoles` WRITE;
/*!40000 ALTER TABLE `PumaUserRoles` DISABLE KEYS */;
/*!40000 ALTER TABLE `PumaUserRoles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `PumaUserTokens`
--

DROP TABLE IF EXISTS `PumaUserTokens`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `PumaUserTokens` (
  `UserId` varchar(255) NOT NULL,
  `LoginProvider` varchar(255) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `Value` longtext,
  PRIMARY KEY (`UserId`,`LoginProvider`,`Name`),
  CONSTRAINT `FK_PumaUserTokens_PumaUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `PumaUsers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `PumaUserTokens`
--

LOCK TABLES `PumaUserTokens` WRITE;
/*!40000 ALTER TABLE `PumaUserTokens` DISABLE KEYS */;
/*!40000 ALTER TABLE `PumaUserTokens` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `PumaUsers`
--

DROP TABLE IF EXISTS `PumaUsers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `PumaUsers` (
  `Id` varchar(255) NOT NULL,
  `UserName` varchar(256) DEFAULT NULL,
  `NormalizedUserName` varchar(256) DEFAULT NULL,
  `Email` varchar(256) DEFAULT NULL,
  `NormalizedEmail` varchar(256) DEFAULT NULL,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `PasswordHash` longtext,
  `SecurityStamp` longtext,
  `ConcurrencyStamp` longtext,
  `PhoneNumber` longtext,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEnd` datetime(6) DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int NOT NULL,
  `MemberId` int NOT NULL AUTO_INCREMENT,
  `CreditCardNumber` longtext,
  `CreditCardExpiration` longtext,
  `BillingAddress1` longtext,
  `BillingAddress2` longtext,
  `BillingCity` longtext,
  `BillingState` longtext,
  `BillingZip` longtext,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  KEY `IX_PumaUsers_MemberId` (`MemberId`),
  KEY `EmailIndex` (`NormalizedEmail`)
) ENGINE=InnoDB AUTO_INCREMENT=1004 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `PumaUsers`
--

LOCK TABLES `PumaUsers` WRITE;
/*!40000 ALTER TABLE `PumaUsers` DISABLE KEYS */;
INSERT INTO `PumaUsers` VALUES ('43535D28-1C14-4D17-BD0B-5E8A7778049E','eric@pumascancom','ERIC@PUMASCANCOM','eric@pumascancom','ERIC@PUMASCANCOM',0,'AQAAAAEAACcQAAAAEPLagdhtLx0E7CnkqaN7p5p4LOdB4HSh6wul/RuLG83PMdpK66beAnUD+opDFRRGIw==','SDZP44CH34EUKVVULNHX4CW6FPZ56DOB','42340044-3468-4763-bfa5-513f650d4a54',NULL,0,0,NULL,1,0,1002,NULL,NULL,NULL,NULL,NULL,NULL,NULL),('6450D269-AA4B-44C4-B49F-486DF151BB52','qiwei@pumascan.com','QIWEI@PUMASCAN.COM','qiwei@pumascan.com','QIWEI@PUMASCAN.COM',0,'AQAAAAEAACcQAAAAEKwfoWOg+c+ejBTM80K+dCeGgJZiedZsWeDHD9y0zwOLc5so8GZXGKMR/eChbTJAkw==','6VF6X5RNBH22VVILYIHNUFYC6PUMW3A6','d29c6ce8-5852-40f3-a5b1-331e611b74be',NULL,0,0,NULL,1,0,1003,NULL,NULL,NULL,NULL,NULL,NULL,NULL),('85D2C08B-750B-4DA9-B55F-ABB8BA6E9634','admin@pumascan.com','ADMIN@PUMASCAN.COM','admin@pumascan.com','ADMIN@PUMASCAN.COM',0,'AQAAAAEAACcQAAAAEIiRtwNmlGFaTG7ubvNUmK9i8HFtMW5jnKhNsM5oD/2+Ma0pS0DEtxOFC48QUBYCug==','2VGOJK3TUHWQSZRHJ4GOTIM7ICMGRVAD','7f8d0f8e-5af7-435d-87f4-66a4c359dbfb',NULL,0,0,NULL,1,0,1001,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
/*!40000 ALTER TABLE `PumaUsers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `SafariUsers`
--

DROP TABLE IF EXISTS `SafariUsers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `SafariUsers` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `SafariId` int NOT NULL,
  `PumaUserId` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_SafariUsers_PumaUserId` (`PumaUserId`),
  KEY `IX_SafariUsers_SafariId_PumaUserId` (`SafariId`,`PumaUserId`),
  CONSTRAINT `FK_SafariUsers_PumaUsers_PumaUserId` FOREIGN KEY (`PumaUserId`) REFERENCES `PumaUsers` (`Id`) ON DELETE RESTRICT,
  CONSTRAINT `FK_SafariUsers_Safaris_SafariId` FOREIGN KEY (`SafariId`) REFERENCES `Safaris` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=21312 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `SafariUsers`
--

LOCK TABLES `SafariUsers` WRITE;
/*!40000 ALTER TABLE `SafariUsers` DISABLE KEYS */;
INSERT INTO `SafariUsers` VALUES (21311,1995,'85D2C08B-750B-4DA9-B55F-ABB8BA6E9634'),(13312,1996,'43535D28-1C14-4D17-BD0B-5E8A7778049E'),(14721,1997,'6450D269-AA4B-44C4-B49F-486DF151BB52');
/*!40000 ALTER TABLE `SafariUsers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Safaris`
--

DROP TABLE IF EXISTS `Safaris`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Safaris` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` longtext,
  `Address` longtext,
  `StartDate` datetime(6) DEFAULT NULL,
  `EndDate` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=1998 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Safaris`
--

LOCK TABLES `Safaris` WRITE;
/*!40000 ALTER TABLE `Safaris` DISABLE KEYS */;
INSERT INTO `Safaris` VALUES (1995,'puma','Sugar Creek DR','1991-01-01 00:00:00.000000','1994-01-20 00:00:00.000000'),(1996,'pumaScan','Delaware ave','1993-02-01 00:00:00.000000','1992-02-15 00:00:00.000000'),(1997,'pumaSecurity','Lincoln Ave','2011-03-01 00:00:00.000000','2019-03-16 00:00:00.000000');
/*!40000 ALTER TABLE `Safaris` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2022-01-18 10:53:57
