/*
 Navicat Premium Data Transfer

 Source Server         : localhost
 Source Server Type    : MySQL
 Source Server Version : 80022
 Source Host           : localhost:3306
 Source Schema         : fyt_sugar

 Target Server Type    : MySQL
 Target Server Version : 80022
 File Encoding         : 65001

 Date: 04/01/2021 22:27:17
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for sys_admin
-- ----------------------------
DROP TABLE IF EXISTS `sys_admin`;
CREATE TABLE `sys_admin`  (
  `Id` bigint(0) NOT NULL COMMENT '唯一编号',
  `RoleGroup` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '所属角色',
  `RoleGroupParent` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '所属角色，包含父级',
  `PostGroup` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '所属岗位',
  `OrganizeId` bigint(0) NOT NULL COMMENT '所属部门',
  `OrganizeIdList` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '所属上级部门组',
  `LoginAccount` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '登录账号',
  `LoginPassWord` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '登录密码',
  `HeadPic` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '头像',
  `FullName` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '姓名',
  `Mobile` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '手机号码',
  `Email` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '邮箱',
  `Sex` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '性别',
  `Status` bit(1) NOT NULL DEFAULT b'1' COMMENT '状态',
  `IsDel` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否删除',
  `Summary` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  `CreateTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `CreateUser` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人',
  `UpdateTime` datetime(0) NULL DEFAULT NULL COMMENT '修改时间',
  `UpdateUser` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '修改人',
  `LoginTime` datetime(0) NULL DEFAULT NULL COMMENT '登录时间',
  `UpLoginTime` datetime(0) NULL DEFAULT NULL COMMENT '上次登录时间',
  `LoginCount` int(0) NOT NULL DEFAULT 0 COMMENT '登录次数',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_admin
-- ----------------------------
INSERT INTO `sys_admin` VALUES (1260380358965334016, '1339756014718816256', '1339755942329323520,1339756014718816256', '1251019168074043392', 1248158071138684928, '1247889786656657408,1248157435479330816,1248158071138684928', 'admins', '1234567', NULL, '李四', '13511111111', '222@qq.com', '男', b'1', b'0', '111111111', '2020-05-13 09:24:29', 'admin', NULL, NULL, NULL, NULL, 0);
INSERT INTO `sys_admin` VALUES (1260394904224403456, '1339756014718816256', '1339755942329323520,1339756014718816256', '1251019168074043392,1251019232876040192', 1248157435479330816, '1247889786656657408,1248157435479330816', 'adminsss', '123456', NULL, 'wangwu', '2132131313', NULL, '男', b'1', b'1', NULL, '2020-05-13 10:22:17', 'admin', NULL, NULL, NULL, NULL, 0);
INSERT INTO `sys_admin` VALUES (1340945043766251520, '1339770084108931072,1339756014718816256', '1339755942329323520,1339770084108931072|1339755942329323520,1339756014718816256', '1251019232876040192', 1248158071138684928, '1247889786656657408,1248157435479330816,1248158071138684928', 'testadmin', 'testadmin', NULL, 'testadmin', '13666666666', '', '男', b'0', b'0', '', '2020-12-21 16:59:47', NULL, NULL, NULL, NULL, NULL, 0);

-- ----------------------------
-- Table structure for sys_authority
-- ----------------------------
DROP TABLE IF EXISTS `sys_authority`;
CREATE TABLE `sys_authority`  (
  `RoleId` bigint(0) NOT NULL COMMENT '角色编号',
  `AdminId` bigint(0) NULL DEFAULT NULL COMMENT '管理员编号',
  `MenuId` bigint(0) NULL DEFAULT NULL COMMENT '菜单编号',
  `BtnFun` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '按钮功能组',
  `Types` int(0) NOT NULL DEFAULT 1 COMMENT '授权类型1=角色-菜单 2=用户-角色 3=角色-菜单-按钮功能'
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_authority
-- ----------------------------

-- ----------------------------
-- Table structure for sys_code
-- ----------------------------
DROP TABLE IF EXISTS `sys_code`;
CREATE TABLE `sys_code`  (
  `Id` bigint(0) NOT NULL COMMENT '唯一编号',
  `TypeId` bigint(0) NOT NULL COMMENT '分类编号',
  `Name` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '字典值名称',
  `CodeValues` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '字典值阈值',
  `Sort` int(0) NOT NULL DEFAULT 1 COMMENT '排序',
  `Status` bit(1) NOT NULL DEFAULT b'1' COMMENT '状态',
  `IsDel` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否删除',
  `Summary` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  `CreateTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `CreateUser` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人',
  `UpdateTime` datetime(0) NULL DEFAULT NULL COMMENT '修改时间',
  `UpdateUser` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '修改人',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '字典信息表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_code
-- ----------------------------
INSERT INTO `sys_code` VALUES (1258662420646334464, 1258640122090491904, '阿迪', 'A', 1, b'1', b'0', NULL, '2020-05-08 15:38:01', 'admin', NULL, NULL);
INSERT INTO `sys_code` VALUES (1258672116346261504, 1258647106323877888, '春季', 'A', 91, b'1', b'0', NULL, '2020-05-08 16:16:32', 'admin', NULL, NULL);
INSERT INTO `sys_code` VALUES (1258672157739847680, 1258647106323877888, '夏季', 'B', 88, b'1', b'0', NULL, '2020-05-08 16:16:42', 'admin', NULL, NULL);
INSERT INTO `sys_code` VALUES (1258672191487217664, 1258647106323877888, '秋季', 'C', 82, b'1', b'0', NULL, '2020-05-08 16:16:50', 'admin', NULL, NULL);
INSERT INTO `sys_code` VALUES (1258672223972102144, 1258647106323877888, '冬季', 'D', 1, b'1', b'0', NULL, '2020-05-08 16:16:58', 'admin', NULL, NULL);
INSERT INTO `sys_code` VALUES (1258675680309284864, 1258647245457330176, '添加', 'Add', 1, b'1', b'0', NULL, '2020-05-08 16:30:42', 'admin', NULL, NULL);
INSERT INTO `sys_code` VALUES (1258675708239155200, 1258647245457330176, '修改', 'Modify', 1, b'1', b'0', NULL, '2020-05-08 16:30:49', 'admin', NULL, NULL);
INSERT INTO `sys_code` VALUES (1258676199962578944, 1258647245457330176, '删除', 'Delete', 1, b'1', b'0', NULL, '2020-05-08 16:32:46', 'admin', NULL, NULL);
INSERT INTO `sys_code` VALUES (1258676306619535360, 1258647245457330176, '审核', 'Audit', 1, b'1', b'0', NULL, '2020-05-08 16:33:11', 'admin', NULL, NULL);
INSERT INTO `sys_code` VALUES (1258676341004439552, 1258647245457330176, '导入', 'Import', 1, b'1', b'0', NULL, '2020-05-08 16:33:19', 'admin', NULL, NULL);
INSERT INTO `sys_code` VALUES (1258676436013813760, 1258647245457330176, '导出', 'Export', 1, b'1', b'0', NULL, '2020-05-08 16:33:42', 'admin', NULL, NULL);

-- ----------------------------
-- Table structure for sys_codetype
-- ----------------------------
DROP TABLE IF EXISTS `sys_codetype`;
CREATE TABLE `sys_codetype`  (
  `Id` bigint(0) NOT NULL COMMENT '唯一编号',
  `ParentId` bigint(0) NOT NULL COMMENT '父节点',
  `ParentIdList` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Layer` int(0) NOT NULL DEFAULT 1 COMMENT '层级',
  `Name` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '分类名称',
  `Sort` int(0) NOT NULL DEFAULT 1 COMMENT '排序',
  `IsSystem` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否系统内置集成',
  `IsDel` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否删除',
  `CreateTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `CreateUser` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人',
  `UpdateTime` datetime(0) NULL DEFAULT NULL COMMENT '修改时间',
  `UpdateUser` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '修改人',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '字典分类信息' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_codetype
-- ----------------------------
INSERT INTO `sys_codetype` VALUES (1258634388292374528, 0, '1258634388292374528', 1, '商品规格', 1, b'0', b'0', '2020-05-08 13:46:37', 'admin', NULL, NULL);
INSERT INTO `sys_codetype` VALUES (1258640122090491904, 1258634388292374528, '1258640122090491904,', 2, '品牌', 1, b'0', b'0', '2020-05-08 14:09:24', 'admin', NULL, NULL);
INSERT INTO `sys_codetype` VALUES (1258647106323877888, 1258634388292374528, '1258647106323877888,', 2, '季节', 1, b'0', b'0', '2020-05-08 14:37:09', 'admin', NULL, NULL);
INSERT INTO `sys_codetype` VALUES (1258647173655040000, 0, '1258647173655040000,', 1, '系统权限', 1, b'1', b'0', '2020-05-08 14:37:25', 'admin', NULL, NULL);
INSERT INTO `sys_codetype` VALUES (1258647245457330176, 1258647173655040000, '1258647173655040000,1258647245457330176,', 2, '按钮功能', 1, b'1', b'0', '2020-05-08 14:37:43', 'admin', NULL, NULL);

-- ----------------------------
-- Table structure for sys_log
-- ----------------------------
DROP TABLE IF EXISTS `sys_log`;
CREATE TABLE `sys_log`  (
  `Id` bigint(0) NOT NULL,
  `LogType` int(0) NOT NULL COMMENT '日志类型  0=登录  1=操作',
  `Module` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '操作模块',
  `OperateType` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '操作类型:例如添加、修改',
  `Method` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '提交类型：get/post/delete',
  `OperateUser` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '操作人',
  `IP` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'IP',
  `Parameters` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL COMMENT '请求参数',
  `Address` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '操作地址',
  `Status` bit(1) NOT NULL DEFAULT b'1' COMMENT '操作状态',
  `Message` varchar(2000) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '详细信息',
  `ReturnValue` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL COMMENT '返回结果',
  `OperateTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '操作时间',
  `Browser` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '浏览器信息',
  `ExecutionDuration` int(0) NOT NULL DEFAULT 0 COMMENT '执行时长',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '登录日志\r\n操作日志\r\n任务日志' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_log
-- ----------------------------
INSERT INTO `sys_log` VALUES (1346099615145005056, 2, 'FytSoa.Services.Api.Controllers.Sys.LoginController', NULL, 'POST', '', '::ffff:192.168.1.10', '{}', '/api/login/userInfo', b'1', NULL, '{\"code\":200,\"msg\":\"success\",\"data\":{\"username\":\"admin\",\"permissions\":[\"admin\"]},\"avatar\":\"https://i.gtimg.cn/club/item/face/img/2/15922_100.gif\"}', '2021-01-04 22:22:12', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36 Edg/87.0.664.66', 0);
INSERT INTO `sys_log` VALUES (1346099616340381696, 2, 'FytSoa.Services.Api.Controllers.LogsController', NULL, 'GET', '', '::ffff:192.168.1.10', '{\"param\":{\"page\":1,\"limit\":10,\"id\":0,\"key\":null,\"status\":\"1\"}}', '/api/logs?key=&status=1&limit=10&page=1', b'1', NULL, '{\"Message\":null,\"Code\":200,\"Data\":{\"CurrentPage\":1,\"TotalPages\":0,\"TotalItems\":0,\"ItemsPerPage\":10,\"Items\":[]}}', '2021-01-04 22:22:13', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36 Edg/87.0.664.66', 3);
INSERT INTO `sys_log` VALUES (1346099843323531264, 1, 'FytSoa.Services.Api.Controllers.Sys.LoginController', NULL, 'POST', '', '::ffff:192.168.1.10', '{}', '/api/login/userInfo', b'1', NULL, '{\"code\":200,\"msg\":\"success\",\"data\":{\"username\":\"admin\",\"permissions\":[\"admin\"]},\"avatar\":\"https://i.gtimg.cn/club/item/face/img/2/15922_100.gif\"}', '2021-01-04 22:23:07', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36 Edg/87.0.664.66', 54);
INSERT INTO `sys_log` VALUES (1346099846297292800, 2, 'FytSoa.Services.Api.Controllers.LogsController', NULL, 'GET', '', '::ffff:192.168.1.10', '{\"param\":{\"page\":1,\"limit\":10,\"id\":0,\"key\":null,\"status\":\"1\"}}', '/api/logs?key=&status=1&limit=10&page=1', b'1', NULL, '{\"Message\":null,\"Code\":200,\"Data\":{\"CurrentPage\":1,\"TotalPages\":0,\"TotalItems\":1,\"ItemsPerPage\":10,\"Items\":[{\"LogType\":0,\"Module\":null,\"OperateType\":null,\"Method\":\"POST\",\"Parameters\":\"{}\",\"OperateUser\":\"\",\"IP\":\"::ffff:192.168.1.10\",\"Address\":\"/api/login/userInfo\",\"Browser\":\"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36 Edg/87.0.664.66\",\"Status\":true,\"Message\":null,\"OperateTime\":\"2021-01-04T22:23:07\",\"ExecutionDuration\":54,\"ReturnValue\":null,\"Id\":\"1346099843323531264\"}]}}', '2021-01-04 22:23:07', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36 Edg/87.0.664.66', 112);
INSERT INTO `sys_log` VALUES (1346099895085436928, 2, 'FytSoa.Services.Api.Controllers.LogsController', NULL, 'GET', '', '::ffff:192.168.1.10', '{\"param\":{\"page\":1,\"limit\":10,\"id\":0,\"key\":null,\"status\":\"2\"}}', '/api/logs?key=&status=2&limit=10&page=1', b'1', NULL, '{\"Message\":null,\"Code\":200,\"Data\":{\"CurrentPage\":1,\"TotalPages\":0,\"TotalItems\":3,\"ItemsPerPage\":10,\"Items\":[{\"LogType\":0,\"Module\":null,\"OperateType\":null,\"Method\":\"GET\",\"Parameters\":\"{\"param\":{\"page\":1,\"limit\":10,\"id\":0,\"key\":null,\"status\":\"1\"}}\",\"OperateUser\":\"\",\"IP\":\"::ffff:192.168.1.10\",\"Address\":\"/api/logs?key=&status=1&limit=10&page=1\",\"Browser\":\"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36 Edg/87.0.664.66\",\"Status\":true,\"Message\":null,\"OperateTime\":\"2021-01-04T22:23:07\",\"ExecutionDuration\":112,\"ReturnValue\":null,\"Id\":\"1346099846297292800\"},{\"LogType\":0,\"Module\":null,\"OperateType\":null,\"Method\":\"GET\",\"Parameters\":\"{\"param\":{\"page\":1,\"limit\":10,\"id\":0,\"key\":null,\"status\":\"1\"}}\",\"OperateUser\":\"\",\"IP\":\"::ffff:192.168.1.10\",\"Address\":\"/api/logs?key=&status=1&limit=10&page=1\",\"Browser\":\"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36 Edg/87.0.664.66\",\"Status\":true,\"Message\":null,\"OperateTime\":\"2021-01-04T22:22:13\",\"ExecutionDuration\":3,\"ReturnValue\":null,\"Id\":\"1346099616340381696\"},{\"LogType\":0,\"Module\":null,\"OperateType\":null,\"Method\":\"POST\",\"Parameters\":\"{}\",\"OperateUser\":\"\",\"IP\":\"::ffff:192.168.1.10\",\"Address\":\"/api/login/userInfo\",\"Browser\":\"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36 Edg/87.0.664.66\",\"Status\":true,\"Message\":null,\"OperateTime\":\"2021-01-04T22:22:12\",\"ExecutionDuration\":0,\"ReturnValue\":null,\"Id\":\"1346099615145005056\"}]}}', '2021-01-04 22:23:19', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36 Edg/87.0.664.66', 5);
INSERT INTO `sys_log` VALUES (1346099906150010880, 2, 'FytSoa.Services.Api.Controllers.LogsController', NULL, 'GET', '', '::ffff:192.168.1.10', '{\"param\":{\"page\":1,\"limit\":10,\"id\":0,\"key\":null,\"status\":\"1\"}}', '/api/logs?key=&status=1&limit=10&page=1', b'1', NULL, '{\"Message\":null,\"Code\":200,\"Data\":{\"CurrentPage\":1,\"TotalPages\":0,\"TotalItems\":1,\"ItemsPerPage\":10,\"Items\":[{\"LogType\":0,\"Module\":null,\"OperateType\":null,\"Method\":\"POST\",\"Parameters\":\"{}\",\"OperateUser\":\"\",\"IP\":\"::ffff:192.168.1.10\",\"Address\":\"/api/login/userInfo\",\"Browser\":\"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36 Edg/87.0.664.66\",\"Status\":true,\"Message\":null,\"OperateTime\":\"2021-01-04T22:23:07\",\"ExecutionDuration\":54,\"ReturnValue\":null,\"Id\":\"1346099843323531264\"}]}}', '2021-01-04 22:23:22', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36 Edg/87.0.664.66', 20);
INSERT INTO `sys_log` VALUES (1346099910147182592, 2, 'FytSoa.Services.Api.Controllers.LogsController', NULL, 'GET', '', '::ffff:192.168.1.10', '{\"param\":{\"page\":1,\"limit\":10,\"id\":0,\"key\":null,\"status\":\"2\"}}', '/api/logs?key=&status=2&limit=10&page=1', b'1', NULL, '{\"Message\":null,\"Code\":200,\"Data\":{\"CurrentPage\":1,\"TotalPages\":0,\"TotalItems\":5,\"ItemsPerPage\":10,\"Items\":[{\"LogType\":0,\"Module\":null,\"OperateType\":null,\"Method\":\"GET\",\"Parameters\":\"{\"param\":{\"page\":1,\"limit\":10,\"id\":0,\"key\":null,\"status\":\"1\"}}\",\"OperateUser\":\"\",\"IP\":\"::ffff:192.168.1.10\",\"Address\":\"/api/logs?key=&status=1&limit=10&page=1\",\"Browser\":\"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36 Edg/87.0.664.66\",\"Status\":true,\"Message\":null,\"OperateTime\":\"2021-01-04T22:23:22\",\"ExecutionDuration\":20,\"ReturnValue\":null,\"Id\":\"1346099906150010880\"},{\"LogType\":0,\"Module\":null,\"OperateType\":null,\"Method\":\"GET\",\"Parameters\":\"{\"param\":{\"page\":1,\"limit\":10,\"id\":0,\"key\":null,\"status\":\"2\"}}\",\"OperateUser\":\"\",\"IP\":\"::ffff:192.168.1.10\",\"Address\":\"/api/logs?key=&status=2&limit=10&page=1\",\"Browser\":\"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36 Edg/87.0.664.66\",\"Status\":true,\"Message\":null,\"OperateTime\":\"2021-01-04T22:23:19\",\"ExecutionDuration\":5,\"ReturnValue\":null,\"Id\":\"1346099895085436928\"},{\"LogType\":0,\"Module\":null,\"OperateType\":null,\"Method\":\"GET\",\"Parameters\":\"{\"param\":{\"page\":1,\"limit\":10,\"id\":0,\"key\":null,\"status\":\"1\"}}\",\"OperateUser\":\"\",\"IP\":\"::ffff:192.168.1.10\",\"Address\":\"/api/logs?key=&status=1&limit=10&page=1\",\"Browser\":\"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36 Edg/87.0.664.66\",\"Status\":true,\"Message\":null,\"OperateTime\":\"2021-01-04T22:23:07\",\"ExecutionDuration\":112,\"ReturnValue\":null,\"Id\":\"1346099846297292800\"},{\"LogType\":0,\"Module\":null,\"OperateType\":null,\"Method\":\"GET\",\"Parameters\":\"{\"param\":{\"page\":1,\"limit\":10,\"id\":0,\"key\":null,\"status\":\"1\"}}\",\"OperateUser\":\"\",\"IP\":\"::ffff:192.168.1.10\",\"Address\":\"/api/logs?key=&status=1&limit=10&page=1\",\"Browser\":\"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36 Edg/87.0.664.66\",\"Status\":true,\"Message\":null,\"OperateTime\":\"2021-01-04T22:22:13\",\"ExecutionDuration\":3,\"ReturnValue\":null,\"Id\":\"1346099616340381696\"},{\"LogType\":0,\"Module\":null,\"OperateType\":null,\"Method\":\"POST\",\"Parameters\":\"{}\",\"OperateUser\":\"\",\"IP\":\"::ffff:192.168.1.10\",\"Address\":\"/api/login/userInfo\",\"Browser\":\"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36 Edg/87.0.664.66\",\"Status\":true,\"Message\":null,\"OperateTime\":\"2021-01-04T22:22:12\",\"ExecutionDuration\":0,\"ReturnValue\":null,\"Id\":\"1346099615145005056\"}]}}', '2021-01-04 22:23:23', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36 Edg/87.0.664.66', 6);
INSERT INTO `sys_log` VALUES (1346099915616555008, 2, 'FytSoa.Services.Api.Controllers.LogsController', NULL, 'GET', '', '::ffff:192.168.1.10', '{\"id\":1346099906150010880}', '/api/logs/1346099906150010880', b'1', NULL, '{\"Message\":null,\"Code\":200,\"Data\":{\"LogType\":2,\"Module\":\"FytSoa.Services.Api.Controllers.LogsController\",\"OperateType\":null,\"Method\":\"GET\",\"Parameters\":\"{\"param\":{\"page\":1,\"limit\":10,\"id\":0,\"key\":null,\"status\":\"1\"}}\",\"OperateUser\":\"\",\"IP\":\"::ffff:192.168.1.10\",\"Address\":\"/api/logs?key=&status=1&limit=10&page=1\",\"Browser\":\"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36 Edg/87.0.664.66\",\"Status\":true,\"Message\":null,\"OperateTime\":\"2021-01-04T22:23:22\",\"ExecutionDuration\":20,\"ReturnValue\":\"{\"Message\":null,\"Code\":200,\"Data\":{\"CurrentPage\":1,\"TotalPages\":0,\"TotalItems\":1,\"ItemsPerPage\":10,\"Items\":[{\"LogType\":0,\"Module\":null,\"OperateType\":null,\"Method\":\"POST\",\"Parameters\":\"{}\",\"OperateUser\":\"\",\"IP\":\"::ffff:192.168.1.10\",\"Address\":\"/api/login/userInfo\",\"Browser\":\"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36 Edg/87.0.664.66\",\"Status\":true,\"Message\":null,\"OperateTime\":\"2021-01-04T22:23:07\",\"ExecutionDuration\":54,\"ReturnValue\":null,\"Id\":\"1346099843323531264\"}]}}\",\"Id\":\"1346099906150010880\"}}', '2021-01-04 22:23:24', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36 Edg/87.0.664.66', 9);
INSERT INTO `sys_log` VALUES (1346099924986630144, 2, 'FytSoa.Services.Api.Controllers.LogsController', NULL, 'GET', '', '::ffff:192.168.1.10', '{\"id\":1346099615145005056}', '/api/logs/1346099615145005056', b'1', NULL, '{\"Message\":null,\"Code\":200,\"Data\":{\"LogType\":2,\"Module\":\"FytSoa.Services.Api.Controllers.Sys.LoginController\",\"OperateType\":null,\"Method\":\"POST\",\"Parameters\":\"{}\",\"OperateUser\":\"\",\"IP\":\"::ffff:192.168.1.10\",\"Address\":\"/api/login/userInfo\",\"Browser\":\"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36 Edg/87.0.664.66\",\"Status\":true,\"Message\":null,\"OperateTime\":\"2021-01-04T22:22:12\",\"ExecutionDuration\":0,\"ReturnValue\":\"{\"code\":200,\"msg\":\"success\",\"data\":{\"username\":\"admin\",\"permissions\":[\"admin\"]},\"avatar\":\"https://i.gtimg.cn/club/item/face/img/2/15922_100.gif\"}\",\"Id\":\"1346099615145005056\"}}', '2021-01-04 22:23:26', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36 Edg/87.0.664.66', 3);
INSERT INTO `sys_log` VALUES (1346099932829978624, 2, 'FytSoa.Services.Api.Controllers.LogsController', NULL, 'GET', '', '::ffff:192.168.1.10', '{\"id\":1346099895085436928}', '/api/logs/1346099895085436928', b'1', NULL, '{\"Message\":null,\"Code\":200,\"Data\":{\"LogType\":2,\"Module\":\"FytSoa.Services.Api.Controllers.LogsController\",\"OperateType\":null,\"Method\":\"GET\",\"Parameters\":\"{\"param\":{\"page\":1,\"limit\":10,\"id\":0,\"key\":null,\"status\":\"2\"}}\",\"OperateUser\":\"\",\"IP\":\"::ffff:192.168.1.10\",\"Address\":\"/api/logs?key=&status=2&limit=10&page=1\",\"Browser\":\"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36 Edg/87.0.664.66\",\"Status\":true,\"Message\":null,\"OperateTime\":\"2021-01-04T22:23:19\",\"ExecutionDuration\":5,\"ReturnValue\":\"{\"Message\":null,\"Code\":200,\"Data\":{\"CurrentPage\":1,\"TotalPages\":0,\"TotalItems\":3,\"ItemsPerPage\":10,\"Items\":[{\"LogType\":0,\"Module\":null,\"OperateType\":null,\"Method\":\"GET\",\"Parameters\":\"{\"param\":{\"page\":1,\"limit\":10,\"id\":0,\"key\":null,\"status\":\"1\"}}\",\"OperateUser\":\"\",\"IP\":\"::ffff:192.168.1.10\",\"Address\":\"/api/logs?key=&status=1&limit=10&page=1\",\"Browser\":\"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36 Edg/87.0.664.66\",\"Status\":true,\"Message\":null,\"OperateTime\":\"2021-01-04T22:23:07\",\"ExecutionDuration\":112,\"ReturnValue\":null,\"Id\":\"1346099846297292800\"},{\"LogType\":0,\"Module\":null,\"OperateType\":null,\"Method\":\"GET\",\"Parameters\":\"{\"param\":{\"page\":1,\"limit\":10,\"id\":0,\"key\":null,\"status\":\"1\"}}\",\"OperateUser\":\"\",\"IP\":\"::ffff:192.168.1.10\",\"Address\":\"/api/logs?key=&status=1&limit=10&page=1\",\"Browser\":\"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36 Edg/87.0.664.66\",\"Status\":true,\"Message\":null,\"OperateTime\":\"2021-01-04T22:22:13\",\"ExecutionDuration\":3,\"ReturnValue\":null,\"Id\":\"1346099616340381696\"},{\"LogType\":0,\"Module\":null,\"OperateType\":null,\"Method\":\"POST\",\"Parameters\":\"{}\",\"OperateUser\":\"\",\"IP\":\"::ffff:192.168.1.10\",\"Address\":\"/api/login/userInfo\",\"Browser\":\"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36 Edg/87.0.664.66\",\"Status\":true,\"Message\":null,\"OperateTime\":\"2021-01-04T22:22:12\",\"ExecutionDuration\":0,\"ReturnValue\":null,\"Id\":\"1346099615145005056\"}]}}\",\"Id\":\"1346099895085436928\"}}', '2021-01-04 22:23:28', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36 Edg/87.0.664.66', 2);

-- ----------------------------
-- Table structure for sys_menu
-- ----------------------------
DROP TABLE IF EXISTS `sys_menu`;
CREATE TABLE `sys_menu`  (
  `Id` bigint(0) NOT NULL COMMENT '唯一编号',
  `Name` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '菜单名称',
  `ParentId` bigint(0) NULL DEFAULT NULL COMMENT '父节点',
  `ParentIdList` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '父节点集合组',
  `Code` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '权限标识',
  `Layer` int(0) NOT NULL DEFAULT 0 COMMENT '菜单层级',
  `Urls` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '路由地址',
  `Icon` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '菜单图标',
  `Sort` int(0) NOT NULL DEFAULT 1 COMMENT '排序',
  `Status` bit(1) NOT NULL DEFAULT b'1' COMMENT '状态',
  `IsDel` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否删除',
  `Types` int(0) NOT NULL DEFAULT 1 COMMENT '菜单类型：1=目录  2=菜单',
  `BtnFun` varchar(1000) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '菜单按钮',
  `CreateTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `CreateUser` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人',
  `UpdateTime` datetime(0) NULL DEFAULT NULL COMMENT '修改时间',
  `UpdateUser` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '修改人',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_menu
-- ----------------------------
INSERT INTO `sys_menu` VALUES (1258686437235232768, '系统管理', 0, '1258686437235232768,', 'Sys', 1, NULL, 'el-icon-orange', 4, b'1', b'0', 1, '[]', '2020-05-08 17:13:27', 'admin', NULL, NULL);
INSERT INTO `sys_menu` VALUES (1258687635354947584, '部门管理', 1258686437235232768, '1258686437235232768,1258687635354947584', 'SysOrganize', 2, 'sys/organize', NULL, 5, b'1', b'0', 2, '[]', '2020-05-08 17:18:12', 'admin', NULL, NULL);
INSERT INTO `sys_menu` VALUES (1260452590072762368, '角色管理', 1258686437235232768, '1258686437235232768,1260452590072762368', 'SysRole', 2, '/sys/role', NULL, 6, b'1', b'0', 2, '[{\"id\":\"1258675680309284864\",\"name\":\"添加\",\"value\":\"Add\"},{\"id\":\"1258675708239155200\",\"name\":\"修改\",\"value\":\"Modify\"},{\"id\":\"1258676199962578944\",\"name\":\"删除\",\"value\":\"Delete\"}]', '2020-05-13 14:11:30', 'admin', NULL, NULL);
INSERT INTO `sys_menu` VALUES (1260452731953483776, '岗位管理', 1258686437235232768, '1258686437235232768,1260452731953483776', 'SysPost', 2, '/sys/post', NULL, 7, b'1', b'0', 2, '[{\"id\":\"1258675680309284864\",\"name\":\"添加\",\"value\":\"Add\"},{\"id\":\"1258675708239155200\",\"name\":\"修改\",\"value\":\"Modify\"},{\"id\":\"1258676199962578944\",\"name\":\"删除\",\"value\":\"Delete\"}]', '2020-05-13 14:12:04', 'admin', NULL, NULL);
INSERT INTO `sys_menu` VALUES (1260452840476905472, '用户管理', 1258686437235232768, '1258686437235232768,1260452840476905472', 'SysUser', 2, '/sys/user', NULL, 8, b'1', b'0', 2, '[{\"id\":\"1258675680309284864\",\"name\":\"添加\",\"value\":\"Add\"},{\"id\":\"1258675708239155200\",\"name\":\"修改\",\"value\":\"Modify\"},{\"id\":\"1258676199962578944\",\"name\":\"删除\",\"value\":\"Delete\"}]', '2020-05-13 14:12:30', 'admin', NULL, NULL);
INSERT INTO `sys_menu` VALUES (1260452945938485248, '菜单管理', 1258686437235232768, '1258686437235232768,1260452945938485248', 'SysMenu', 2, '/sys/menu', NULL, 9, b'1', b'0', 2, '[{\"id\":\"1258675680309284864\",\"name\":\"添加\",\"value\":\"Add\"},{\"id\":\"1258675708239155200\",\"name\":\"修改\",\"value\":\"Modify\"},{\"id\":\"1258676199962578944\",\"name\":\"删除\",\"value\":\"Delete\"},{\"id\":\"1258676306619535360\",\"name\":\"审核\",\"value\":\"Audit\"}]', '2020-05-13 14:12:55', 'admin', NULL, NULL);
INSERT INTO `sys_menu` VALUES (1339869098296872960, '首页', 0, '1339869098296872960,', 'Home', 1, NULL, 'el-icon-s-home', 1, b'1', b'0', 1, '[]', '2020-12-18 17:44:21', NULL, NULL, NULL);
INSERT INTO `sys_menu` VALUES (1339869568155389952, '工作台', 1339869098296872960, '1339869098296872960,1339869568155389952,', 'Workbench', 2, 'home/workbench', NULL, 2, b'1', b'0', 2, '[{\"id\":\"1258676306619535360\",\"name\":\"审核\",\"value\":\"Audit\"},{\"id\":\"1258676341004439552\",\"name\":\"导入\",\"value\":\"Import\"},{\"id\":\"1258675680309284864\",\"name\":\"添加\",\"value\":\"Add\"}]', '2020-12-18 17:46:13', NULL, NULL, NULL);
INSERT INTO `sys_menu` VALUES (1339869712040988672, '数据中心', 1339869098296872960, '1339869098296872960,1339869712040988672,', 'DataCenter', 2, 'home/data', NULL, 3, b'1', b'0', 2, '[{\"id\":\"1258676341004439552\",\"name\":\"导入\",\"value\":\"Import\"},{\"id\":\"1258676436013813760\",\"name\":\"导出\",\"value\":\"Export\"}]', '2020-12-18 17:46:13', NULL, NULL, NULL);
INSERT INTO `sys_menu` VALUES (1339870114027278336, '日志管理', 1258686437235232768, '1339870114027278336,', 'Logs', 2, 'sys/logs', NULL, 10, b'1', b'0', 2, '[]', '2020-12-18 17:46:13', NULL, NULL, NULL);
INSERT INTO `sys_menu` VALUES (1340906599023251456, '系统设置', 1258686437235232768, '1258686437235232768,1340906599023251456,', 'SysSetting', 2, 'sys/setting', NULL, 11, b'1', b'0', 2, '[]', '2020-12-21 14:27:01', NULL, NULL, NULL);

-- ----------------------------
-- Table structure for sys_organize
-- ----------------------------
DROP TABLE IF EXISTS `sys_organize`;
CREATE TABLE `sys_organize`  (
  `Id` bigint(0) NOT NULL COMMENT '唯一编号',
  `ParentId` bigint(0) NOT NULL COMMENT '父节点',
  `Name` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '部门名称',
  `Number` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '机构编码',
  `ParentIdList` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '父节点集合',
  `Layer` int(0) NOT NULL DEFAULT 0 COMMENT '部门层级',
  `Sort` int(0) NOT NULL DEFAULT 1 COMMENT '排序',
  `Status` bit(1) NOT NULL DEFAULT b'1' COMMENT '状态',
  `IsDel` bit(1) NOT NULL DEFAULT b'0' COMMENT '删除状态',
  `LeaderUser` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '部门负责人',
  `LeaderMobile` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '联系电话',
  `LeaderEmail` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '联系邮箱',
  `CreateTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `CreateUser` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人',
  `UpdateTime` datetime(0) NULL DEFAULT NULL COMMENT '修改时间',
  `UpdateUser` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '修改人',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_organize
-- ----------------------------
INSERT INTO `sys_organize` VALUES (1247889786656657408, 0, 'Fyt集团', NULL, '1247889786656657408,', 1, 28, b'1', b'0', '1', '111', '1111', '2020-04-08 22:11:24', 'admin', NULL, NULL);
INSERT INTO `sys_organize` VALUES (1248157435479330816, 1247889786656657408, '行政/人事', NULL, '1247889786656657408,1248157435479330816,', 2, 1, b'1', b'0', '李四', '13688888888', '58555@qq.com', '2020-04-09 15:54:57', 'admin', NULL, NULL);
INSERT INTO `sys_organize` VALUES (1248158071138684928, 1248157435479330816, '人事部', '11111111', '1247889786656657408,1248157435479330816,1248158071138684928,', 3, 1, b'1', b'0', '李总', '16544444444', '55545444@qq.com', '2020-04-09 15:57:28', 'admin', NULL, NULL);

-- ----------------------------
-- Table structure for sys_post
-- ----------------------------
DROP TABLE IF EXISTS `sys_post`;
CREATE TABLE `sys_post`  (
  `Id` bigint(0) NOT NULL COMMENT '唯一编号',
  `Name` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '岗位名称',
  `Number` varchar(6) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '岗位编码',
  `Sort` int(0) NOT NULL DEFAULT 1 COMMENT '排序',
  `Status` bit(1) NOT NULL DEFAULT b'1' COMMENT '岗位状态',
  `IsDel` bit(1) NOT NULL DEFAULT b'0' COMMENT '删除状态',
  `Summary` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注信息',
  `CreateTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `CreateUser` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人',
  `UpdateTime` datetime(0) NULL DEFAULT NULL COMMENT '修改时间',
  `UpdateUser` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '修改人',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_post
-- ----------------------------
INSERT INTO `sys_post` VALUES (1251019168074043392, '项目经理', '100001', 1, b'1', b'0', '项目经理', '2020-04-17 13:26:27', 'admin', NULL, NULL);
INSERT INTO `sys_post` VALUES (1251019232876040192, '测试经理', '100002', 37, b'1', b'0', '测试经理', '2020-04-17 13:26:27', 'admin', NULL, NULL);

-- ----------------------------
-- Table structure for sys_role
-- ----------------------------
DROP TABLE IF EXISTS `sys_role`;
CREATE TABLE `sys_role`  (
  `Id` bigint(0) NOT NULL COMMENT '唯一编号',
  `Name` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '角色名称',
  `ParentId` bigint(0) NULL DEFAULT NULL COMMENT '角色父节点',
  `ParentIdList` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '父节点结合',
  `Layer` int(0) NOT NULL DEFAULT 1 COMMENT '角色层级',
  `Number` varchar(6) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '角色编号',
  `IsSystem` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否超级管理员',
  `Status` bit(1) NOT NULL DEFAULT b'1' COMMENT '状态',
  `Sort` int(0) NOT NULL DEFAULT 1 COMMENT '排序',
  `Summary` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '描述',
  `IsDel` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否删除',
  `CreateTime` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `CreateUser` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人',
  `UpdateTime` datetime(0) NULL DEFAULT NULL COMMENT '修改时间',
  `UpdateUser` char(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '修改人',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_role
-- ----------------------------
INSERT INTO `sys_role` VALUES (1339755942329323520, '管理组', 0, '1339755942329323520,', 1, '123', b'0', b'1', 1, NULL, b'0', '2020-12-18 10:14:42', NULL, NULL, NULL);
INSERT INTO `sys_role` VALUES (1339756014718816256, '超级管理员', 1339755942329323520, '1339755942329323520,1339756014718816256,', 2, '456', b'1', b'1', 1, NULL, b'0', '2020-12-18 10:15:00', NULL, NULL, NULL);
INSERT INTO `sys_role` VALUES (1339770084108931072, '开发管理员', 1339755942329323520, '1339755942329323520,1339770084108931072,', 2, '789', b'0', b'1', 1, NULL, b'0', '2020-12-18 11:10:54', NULL, NULL, NULL);

SET FOREIGN_KEY_CHECKS = 1;
