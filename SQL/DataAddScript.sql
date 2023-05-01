USE SalesServiceAppDB
GO

INSERT INTO [dbo].[Categories]
           ([Title])
     VALUES
           ('�����������'),
		   ('�������� � ���������� ����������'),
		   ('��������'),
		   ('���������� �������'),
		   ('��������'),
		   ('���������� ���������'),
		   ('�������������� �����������'),
		   ('���� ����������, ���������, ����������')
GO

INSERT INTO [dbo].[Products]
           ([Title],[Description],[Picture],[Cost],[Discount],[DateOfAdd])
     VALUES
			--�����������
           ('������ ����','�������� ���� �������� ��������, �������� � ���������� ��������, ���������� �����, ������������������� � �������� ����, ��� ��������� ���������������� ����� ���������� � ������� �������. ����� ��������� ������������ ���������� ������������������ ����������, �������� ��� ����� ������� ���������������.','product_1.png',13000,0,'13-04-2012'),
		   ('����','����������� ��� ����������� ���, ��� � ��� �������������� ����������������. ������������ ��������������������� �����, �������� ��� ����������� �� ������� ������������ ���������� �������������, � ������� ������������ ���� �� ���������� ������������ ��� �������������� �������������� ����� ������.','product_1.png',33600,0,'13-04-2012'),
		   ('������� ������','����� ������� � �������� ���������, ��������� �� ������� ������ � ����� ����������������. ������ ���������� �������� ����� ����������������, ��� ��������� ��������� ����� � �������� ������. ��������� ��������� ������ ����������, �������� ���� ����������� ������������ ��� ������� �������� ��������, ��� ���� ��������� ������� ���������.','product_1.png',5400,0,'13-04-2012'),
		   ('�������� �� 5 �������������','��� ������� 1�:����������� 8. �������� �� 5 ������������� �� ��������� �� ��������� � �������� �������� 1� ����������� 8 ���� � �������� �� 5 ������� ����.','product_2.png',26000,0,'23-04-2023'),
		   ('����','�������������� ����������� ��������� ����� ������������������� ������������. ������ ���� ������������ ���������� ��������, � ����� � ���� � ��������� ��� ����������� ��������, � ������������ � ������������ �� ��.','product_3.png',43200,0,'13-04-2012'),
		   ('����','��������� �������� � ���� ��������� �1�:����������� 8.3� � ������������ ������������ �������������� �����������. ��� ����������� � ������������ � �������� ������� �� ��� ����������� ����� ������ �������������� ����� ���������-������������� ������������ ����������� � ���������� �� ��� ����������� �� 31.10.2000 � 94�.','product_4.png',14400,0,'15-04-2012'),
		   ('���. ������� ������','������� �1�:����������� ��Ҕ ������������� ��� ������������� �������������� � ���������� �����, ������� ���������� ������������ (������������������) ���������� � �������������/�������������� �������������� �������������, ������ ������������, ���������� �������� � �������-������������ (��������) ������������ (���), ������� ���� � �������� �������������� �����������.','product_5.png',7200,0,'15-04-2012'),
		   ('��������� 8','����������� ������� �1� ��������� 8� � ��� ��������������������, ������������������ �������� ������������ �1�:����������� 8. ������� ������� ��� �������� � ����������� ������� �������������� � ���������� ����� �� ���, � ����� ��� ���������� ������������������ ����������.','product_6.png',5400,0,'15-04-2012'),

		   --�������� � ���������� ���������v
		   ('����','� ��������� 1�:�������� � ���������� ���������� 8 ���� ����� ������������ ���������� ���� ������������ ����� ����� � � ���� �� ��������� ������ � ����������: ����������������� �������� �������� ����������, �������� ����������� ������������ � ���� HR �������.','product_7.png',100900,0,'18-04-2012'),
		   ('����','������ ������� ������� �����������, � ������ �������, ��� �������� ����� � ����������� ������� �����������, ���� ���������� � ������ � ������������.','product_7.png',22600,0,'18-04-2012'),
		   ('������� ������','������ ��������� � ��� ������� ������� � ������� ������������. Ÿ �� ����� ������������ � ������������ ����������� �����������, � ����� ����� ������ ������������ ��������� ��������� ������� ��� ������ ������ ����������������.','product_7.png',8100,0,'18-04-2012'),

		   --��������
		   ('�������','(���������)','product_8.png',3600,0,'20-04-2012'),
		   ('���������� ���������','������� ��������� ����������� ��������� ���� ������������� �������, ��������� ����������� ������ ������������� �������� ������������ � ����������� �������� �������� �����������, � ����� ��������� ���������, ��������, ����������, �������������� � ���������� ��������.','product_9.png',22600,50,'20-04-2012'),
		   ('������� ������','���������� ������� 1�:���������� ��������� 8. ������� ������ ������������ ����������� ������������ ��������� �������: �� ������ � �������� ������, �� ����������� ��������� �����������, �� ������� ������ �������, ������� � ���������� ����� ������������ ��� ���������� ������ ������� �������� ������������ �����������.','product_9.png',7200,70,'20-04-2012'),

		   --���������� �������
		   ('�����:����� ���������� ����������','����������� ������� �1�-�����:����� ���������� ����������. ������ 5.5� ��� ���������� ��������� �������� ����� ���������� � ��������� �1�-�����:�������� ��� ��������� ����������. ������ 5.5�.','product_5.png',8000,0,'10-05-2012'),
		   ('����� �������','������������ ������������� ����� � ���������� ��� ����� ��� � ��������� ������� �������, ���������� � ��������� �������, ������ ������� � ��������������.','product_12.png',25000,0,'20-04-2023'),
		   ('����������','�������� ���������� �������� ��� ������������� ���������� ������� ��������� ������������, ��������, ������� ������������ ������������ �����������.','product_10.png',28000,0,'20-04-2023'),
		   ('����������','��������� ��� ������������� ������������ ��������� ������ ���� � ����������.','product_11.png',30000,0,'10-05-2012'),

		   --��������
		   ('����. ���������� �������� �� ������� �����','�������������� ���������� �������� ������������ ����������� ������ ������� ���������� ������������� � �������������� �� ������ ���������� ������� ����: 1, 5, 10, 20, 50, 100, 300 ��� 500.','product_13.png',6300,0,'18-04-2012'),
		   ('����������� ������������� ��������','�1�:����������� 8. ����������� ������������� ��������� (����� 1�:���) ����������� �� ������������ ����, � ������� �������� ����� ������������ ������ � ������ ��������� �1�:����������� 8 ���ϻ, ���������� �� ��������� �������� 1�:���.','product_5.png',300000,0,'18-04-2012'),
		   ('����. �������� �� ������','�������� ��� ������������� ������-���������� �������� ������ �� ������������ �������� � �������� �������. ��� ��������, ������� ��������� ������������� ������� ���������� ������� ���� �� ����������� ������ �������� �� ������ ������ ����. ��� ����� ����� ������� ����������� ����������, ������� ���������� ��������� �� ��������� ������������.','product_5.png',50400,0,'18-04-2012'),

		   --���������� ���������
		   ('����������� �������������','������ ��������� � ��� ������� ������� � ������� ������������. Ÿ �� ����� ������������ � ������������ ����������� �����������, � ����� ����� ������ ������������ ��������� ��������� ������� ��� ������ ������ ����������������.','product_14.png',61700,90,'26-04-2012'),
		   ('���������� ����� ������','������� ����������� ������� ��� ����������� ������ �������. � ��������� ����������� ��� ����������� �������� ��� ������������� �����, �������, ������������ � ��������. ','product_15.png',17400,0,'26-04-2012'),

		   --�������������� �����������

		   --���� ����������, ���������, ���������
		   ('�������������� ���������� ����� �� ��������� ������������� ������� ����� ������� ��� 1�:�����������','�������������� ���������� ����� �� ��������� ������������� ������� ����� ������� �� ��� ���������, �� ��������� ������� ��� ����������� ���������� ���������� �������. ����� ��� ������ ��������� ������� ���� �� ������� ����� �������������.','product_16.png',16800,0,'18-04-2012'),
		   ('����� ������������������� ��������� � ����','������� ����� ��� 1�:����������� ������������������ ��������� � ������������ �������� �������, � ���������� ������������ ��� ���� �����: ��� ����������� ����� ��������� ������� ���� ��������� �������������� � �������� ��� �������� ��� ��� ���� ������ ������. ���� ����������� �� �������� �����������.','product_16.png',9600,0,'18-04-2012'),
		   ('������� ����� ��� 1�: ����������� ��������������������� �����������','��������� ����� ��������� ������������: ������� ����� ��� 1�: ����������� ��������������������� �����������, ��������� �������� ����� ��������, ��� ����� �������, ���� �������, ������������ �������������, ��������� ������ �������� �������, ������� � ����.','product_16.png',7800,0,'18-04-2012')
GO

INSERT INTO [dbo].[ProductCategories]
           ([ProductID],[CategoryId])
     VALUES
           (1,1),
		   (2,1),
		   (3,1),
		   (4,1),
		   (5,1),
		   (6,1),
		   (7,1),
		   (8,1),
		   (9,2),
		   (10,2),
		   (11,2),
		   (12,3),
		   (13,3),
		   (14,3),
		   (15,4),
		   (16,4),
		   (17,4),
		   (18,4),
		   (19,5),
		   (20,5),
		   (21,5),
		   (22,6),
		   (23,6),
		   (5,7),
		   (24,8),
		   (25,8)
GO

INSERT INTO [dbo].[Services]
           ([Title],[Description],[CostPerHour],[DateOfAdd],[Picture])
     VALUES
           ('������������ 1�','���������� ���������, �������� �������, ������ � ��������� - ������ ������ �������������� �� ��������� ����� ����������� ��������� 1�. ������ ��������� ������� ������, ������������ ���������, ����� �������������� ��������� � ������. ����������� ���������� � �������, �������24, � ��������������,  � �����������.',1600,'14-05-2012','service_1.png'),
		   ('������ 1�','������ 1� � ��� ����� �����������, ������������ �� ��, ����� 1� � �������� ����������.',1600,'14-05-2012','service_2.png'),
		   ('��������� 1�','��������� �������� 1�, ���������� � ������� �����������, ���������� ���������, ���� ������� ��� ������� � ����.',0,'14-05-2012','service_3.png'),
		   ('�������� ������������� 1�','������� ��������� ��� ���������� �������� � ���������� ������� � ������� �������������. ���� ����� ������ � ���� ������������� ��� ������� ������ �������.',0,'14-05-2012','service_4.png'),
		   ('������������ 1�','��������� 1� � ���������������� ������� �������� �� ������������� 1� ��� ����������� �� ���������� ���������. �� ������������� ����������� �����.',1600,'14-05-2012','service_5.png')
GO

INSERT INTO [dbo].[Clients]
           ([LastName],[FirstName],[MiddleName],[Email],[Phone],[Login],[Password])
     VALUES
           ('������','���������','��������','email@nail.ru','87645738671','client1','client1'),
		   ('��������','���','�������������','yaunoilauyaula-9754@mail.ru','89547272510','client2','client2'),
		   ('�������','���������','����������','seifewautrede-8097@mail.ru','89388179826','client3','client3'),
		   ('��������','���������','���������','bripricequaubro-9315@mail.ru','89354168412','client4','client4'),
		   ('���������','�����','��������������','xiveyettoiba-9717@mail.ru','89125017985','client5','client5')
GO

INSERT INTO [dbo].[Departments]
           ([Title])
     VALUES
           ('��������'),
		   ('���������� ���������'),
		   ('����� ����������'),
		   ('����� ������'),
		   ('����� ����������'),
		   ('����� ������'),
		   ('����� �������� '),
		   ('����������� ���������')
GO

INSERT INTO [dbo].[Employees]
           ([LastName],[FirstName],[MiddleName],[Login],[Password],[DepartmentId])
     VALUES
           ('������','ϸ��','��������','emp1','emp1',1),
		   ('��������','������','��������������','emp2','emp2',2),
		   ('�������','�������','����������','emp3','emp3',3),
		   ('�������','�����','���������','emp4','emp4',4),
		   ('�����','������','���������','emp5','emp5',5),
		   ('��������','��������','���������','emp6','emp6',6),
		   ('������������','��������','�����������','emp7','emp7',7),
		   ('��������','������','�����������','emp8','emp8',8)
GO

INSERT INTO [dbo].[ProductOrder]
           ([DateOfAdd],[DateOfComplete],[Status],[ProductId],[ClientId],[EmployeeId],[PaymentAmount])
     VALUES
           ('29-04-2023','29-04-2023','��������',2,1,3,5000),
		   ('28-04-2023',null,'��������',10,1,null,6000),
		   ('29-04-2023',null,'����������',2,3,5,7000),
		   ('25-03-2023',null,'��������',20,4,null,5300),
		   ('29-03-2023','29-04-2023','��������',2,2,2,5500),
		   ('20-03-2023',null,'��������',12,5,null,4400),
		   ('29-03-2023',null,'����������',5,4,1,3500),
		   ('17-03-2023',null,'��������',8,4,null,2400)
GO

INSERT INTO [dbo].[ServiceOrders]
           ([DateOfAdd],[DateOfComplete],[Status],[ServiceId],[ClientId],[EmployeeId],[PaymentAmount])
     VALUES
           ('29-04-2023','29-04-2023','��������',2,1,1,3500),
		   ('27-04-2023','29-04-2023','��������',2,1,null,null),
		   ('28-04-2023','30-04-2023','����������',3,4,1,null),
		   ('27-04-2023','29-04-2023','��������',4,1,null,null),
		   ('29-04-2023','29-04-2023','��������',5,3,5,3500),
		   ('27-04-2023','29-04-2023','����������',2,1,2,null)
GO

INSERT INTO [dbo].[News]
           ([PublicationDate],[Header],[Text],[EmployeeId])
     VALUES
           ('29-04-2023','������ �� ������!','� ������������ ��� �� �������� ������� ��������� ������!',1),
		   ('30-04-2023','��������� ����� ������!','��� ����������� ���������� ������ ��������!',1)
GO

INSERT INTO [dbo].[Questions]
           ([Topic],[Text],[ClientId],[DateOfAdd],[Answer],[EmployeeId])
     VALUES
           ('�����������','��� ������������ ������ ���������� �� ����������� ��������?',1,'30-04-2023',null,null),
		   ('�����������������','��� ������ ������ �� ������� � �������������� ������� 1�:���?',2,'29-04-2023','���������� ��������� ��������� ������ ����������� ���������� �� ����� �������������� ������� 1�:���, ����� ������ ��� ������ �������� �� ������� ����',1),
		   ('�����������������','��� �������� ��������� ��������� �������� ���������� � ���������� � ������������?',3,'28-04-2023','��� ���������� ��������� �������� ���������� ���������� ��� ��������� ������������ � ��������� ���������� �������� � ������ ��� ��������� / �������� �����������, ������� ������ ������� ���� ����������� ���� � ������� ������� "�������� ����������...", � ����������� ����� �������� �������� ���������, �������� ������� ����� ��������, � ������� �� ����� ��������, ��������� ��������� �� ������ "�������� ����������".',4),
		   ('������������','��� ������������ ����� �� ����������� �������������?',4,'29-03-2023',null,null)
GO

INSERT INTO [dbo].[Reviews]
           ([Text],[Grade],[ClientId],[DateOfAdd])
     VALUES
           ('����� ������� �������� � 2014 �. ��������� ������ �� ������������� � ������������ ������������ �������� 1� ������������ ���������������� �����������. �� ��� ����� ����� ������� �������� ��������������� ���� ������������� � ������������� �������. ���������� �������� �������� ������� ������������� � ��������� � ������� � ������� ����� ����������� �������� � �����.',5,1,'26-04-2023'),
		   ('������� ��� �������� ������������� �� �������, �������������� ������ �������� ���������� , �� ���������� ���������� ������ �� ������ � ������� ����������� ����� � ��������� 1 � �����������, �� � � ������������� ������������ ��� �������� 2018 ���� � ����� �������.',5,4,'27-04-2023'),
		   ('�� ������������ � ��� ������� ����������� �� ������ ���. ��� ���������� ������ ������� 1� ���������������� �������� �����, ����������� �������� ������� ��������, � ����� ������� ���������� ���������: �����������, �������������, ���������� ��������. � �� ������� �� ������ ���������� ������� � 1� 8.2 ����������� �� 1� 8.3 �����������, ��� ��������� �������������� ������ �������.',5,2,'29-04-2023'),
		   ('�� ���������� �������� ����� ��� ������� �������� �� ������������� ������������� ��������� �1�:��ӻ � ��������� � ������ � 2013 ����. ���������� � ����� ��������� � ��� �������� �� ������ ������ ��������� � ������������ � ��������������. �� ���������� 5-� ��� ������ � ��������� ������� �������� �� �� ���� �� �������� � ������������ ������ ������������� ��������.',4,3,'28-04-2023')
GO

