namespace ContractsAndJobs.UnitTests.DataService;

using DataServices.Attributes;
using DataServices.Services;
using System;
using System.Collections.Generic;

public class DataServiceTests
{
    [Fact]
    public void GetDataTable_ThrowsCorrectException_WhenDuplicateOrderIndexesArePresent()
    {
        var data = new List<ClassWithDuplicateOrderIndexes> { new ClassWithDuplicateOrderIndexes() };
        var service = new DataService();

        var exception = Assert.Throws<ArgumentException>(() => service.GetDataTable(data));

        Assert.Equal("Duplicate DataTablePropertyAttributes.OrderIndex found in data.", exception.Message);
    }

    [Fact]
    public void GetDataTable_ThrowsCorrectException_WhenOrderIndexesLessThanZeroArePresent()
    {
        var data = new List<ClassWithOrderIndexLessThanZero> { new ClassWithOrderIndexLessThanZero() };
        var service = new DataService();

        var exception = Assert.Throws<ArgumentException>(() => service.GetDataTable(data));

        Assert.Equal("DataTablePropertyAttributes.OrderIndex less than zero found in data.", exception.Message);
    }

    [Fact]
    public void GetDataTable_ThrowsCorrectException_WhenNoPropertiesDecoratedWithDataTablePropertyAttributes()
    {
        var data = new List<ClassWithNoDataTablePropertyAttributes> { new ClassWithNoDataTablePropertyAttributes() };
        var service = new DataService();

        var exception = Assert.Throws<ArgumentException>(() => service.GetDataTable(data));

        Assert.Equal("No properties decorated with DataTablePropertyAttribute found in data.", exception.Message);
    }

    [Fact]
    public void GetDataTable_ReturnsTableWithCorrectColumnNames()
    {
        var data = new List<ClassWithCorrectDataTablePropertyAttributes>
        {
            new ClassWithCorrectDataTablePropertyAttributes()
        };
        var service = new DataService();

        var dataTable = service.GetDataTable(data);

        Assert.Equal("Id", dataTable.Columns[0].ColumnName);
        Assert.Equal("Name", dataTable.Columns[1].ColumnName);
        Assert.Equal("Description", dataTable.Columns[2].ColumnName);
    }

    [Fact]
    public void GetDataTable_ReturnsTableWithCorrectColumnTypes()
    {
        var data = new List<ClassWithCorrectDataTablePropertyAttributes>
        {
            new ClassWithCorrectDataTablePropertyAttributes()
        };
        var service = new DataService();

        var dataTable = service.GetDataTable(data);

        Assert.Equal("Int32", dataTable.Columns[0].DataType.Name);
        Assert.Equal("String", dataTable.Columns[1].DataType.Name);
        Assert.Equal("String", dataTable.Columns[2].DataType.Name);
    }

    [Fact]
    public void GetDataTable_ReturnsTableWithCorrectColumnNamesInCorrectOrder()
    {
        var data = new List<ClassWithCorrectDataTablePropertyAttributesNotInOrder>
        {
            new ClassWithCorrectDataTablePropertyAttributesNotInOrder()
        };
        var service = new DataService();

        var dataTable = service.GetDataTable(data);

        Assert.Equal("Id", dataTable.Columns[2].ColumnName);
        Assert.Equal("Name", dataTable.Columns[0].ColumnName);
        Assert.Equal("Description", dataTable.Columns[1].ColumnName);
    }

    [Fact]
    public void GetDataTable_ReturnsTableWithCorrectColumnTypesInCorrectOrder()
    {
        var data = new List<ClassWithCorrectDataTablePropertyAttributesNotInOrder>
        {
            new ClassWithCorrectDataTablePropertyAttributesNotInOrder()
        };
        var service = new DataService();

        var dataTable = service.GetDataTable(data);

        Assert.Equal("Int32", dataTable.Columns[2].DataType.Name);
        Assert.Equal("String", dataTable.Columns[0].DataType.Name);
        Assert.Equal("String", dataTable.Columns[1].DataType.Name);
    }

    [Fact]
    public void GetDataTable_ReturnsCorrectRows()
    {
        var data = new List<ClassWithCorrectDataTablePropertyAttributes>
        {
            new ClassWithCorrectDataTablePropertyAttributes{Id = 1, Name = "Glen", Description = "Glen's Description Text", Dob = new DateTime(2023, 2, 25), NullableDob = null },
            new ClassWithCorrectDataTablePropertyAttributes{Id = 2, Name = "Jana", Description = "Jana's Description Text", Dob = new DateTime(2022, 2, 25) },
            new ClassWithCorrectDataTablePropertyAttributes{Id = 3, Name = "Katie", Description = "Katie's Description Text", Dob = new DateTime(2021, 2, 25), NullableDob = null },
            new ClassWithCorrectDataTablePropertyAttributes{Id = 4, Name = "Adam", Description = "Adam's Description Text", Dob = new DateTime(2020, 2, 25), NullableDob = new DateTime(2020, 2, 25) }
        };
        var service = new DataService();

        var dataTable = service.GetDataTable(data);

        Assert.Equal(1, dataTable.Rows[0].ItemArray[0]);
        Assert.Equal("Glen", dataTable.Rows[0].ItemArray[1]);
        Assert.Equal("Glen's Description Text", dataTable.Rows[0].ItemArray[2]);
        Assert.Equal(new DateTime(2023, 2, 25), dataTable.Rows[0].ItemArray[3]);
        Assert.Equal(DBNull.Value, dataTable.Rows[0].ItemArray[4]);

        Assert.Equal(2, dataTable.Rows[1].ItemArray[0]);
        Assert.Equal("Jana", dataTable.Rows[1].ItemArray[1]);
        Assert.Equal("Jana's Description Text", dataTable.Rows[1].ItemArray[2]);
        Assert.Equal(new DateTime(2022, 2, 25), dataTable.Rows[1].ItemArray[3]);
        Assert.Equal(DBNull.Value, dataTable.Rows[1].ItemArray[4]);

        Assert.Equal(3, dataTable.Rows[2].ItemArray[0]);
        Assert.Equal("Katie", dataTable.Rows[2].ItemArray[1]);
        Assert.Equal("Katie's Description Text", dataTable.Rows[2].ItemArray[2]);
        Assert.Equal(new DateTime(2021, 2, 25), dataTable.Rows[2].ItemArray[3]);
        Assert.Equal(DBNull.Value, dataTable.Rows[2].ItemArray[4]);

        Assert.Equal(4, dataTable.Rows[3].ItemArray[0]);
        Assert.Equal("Adam", dataTable.Rows[3].ItemArray[1]);
        Assert.Equal("Adam's Description Text", dataTable.Rows[3].ItemArray[2]);
        Assert.Equal(new DateTime(2020, 2, 25), dataTable.Rows[3].ItemArray[3]);
        Assert.Equal(new DateTime(2020, 2, 25), dataTable.Rows[3].ItemArray[4]);
    }

    public class ClassWithDuplicateOrderIndexes
    {
        [DataTableProperty(1)]
        public int Id { get; set; }
        [DataTableProperty(2)]
        public string? Name { get; set; }
        [DataTableProperty(1)]
        public string? Description { get; set; }
    }

    public class ClassWithOrderIndexLessThanZero
    {
        [DataTableProperty(1)]
        public int Id { get; set; }
        [DataTableProperty(-2)]
        public string? Name { get; set; }
        [DataTableProperty(3)]
        public string? Description { get; set; }
    }

    public class ClassWithNoDataTablePropertyAttributes
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }

    public class ClassWithCorrectDataTablePropertyAttributes
    {
        [DataTableProperty(1)]
        public int Id { get; set; }
        [DataTableProperty(2)]
        public string? Name { get; set; }
        [DataTableProperty(3)]
        public string? Description { get; set; }
        [DataTableProperty(4)]
        public DateTime Dob { get; set; }
        [DataTableProperty(5)]
        public DateTime? NullableDob { get; set; }
    }

    public class ClassWithCorrectDataTablePropertyAttributesNotInOrder
    {
        [DataTableProperty(3)]
        public int Id { get; set; }
        [DataTableProperty(1)]
        public string? Name { get; set; }
        [DataTableProperty(2)]
        public string? Description { get; set; }
    }
}
