using System;
using System.Collections.Generic;
using System.Linq;
using itemratingsapi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

[Route("api/[controller]")]
public class ItemsController : Controller
{
    private WebAPIDataContext _webAPIDataContext;
    public ItemsController(WebAPIDataContext webAPIDataContext)
    {
        _webAPIDataContext = webAPIDataContext;
    }

	/// <summary>
    ///     Gets the entire list of Items.
    /// </summary>
    /// <returns>String array</returns>
    /// <response code="200">String array with two values</response>
    /// <response code="400">Error model</response> 
    [HttpGet]
    public IEnumerable<Item> Get()
    {
        return _webAPIDataContext.Items.AsEnumerable();
    }

	/// <summary>
    ///     Gets string array describing an Item.
    /// </summary>
    /// <param name="id">The id of the Item.</param>
    /// <returns>String array</returns>
    /// <response code="200">String array with two values</response>
    /// <response code="400">Error model</response> 
    [HttpGet("{id}")]
    public Item Get(int id)
    {
        return _webAPIDataContext.Items.FirstOrDefault(x => x.Id == id);
    }

    [HttpPost]
    public void Post([FromBody]Item item)
    {
        _webAPIDataContext.Add(item);
        _webAPIDataContext.SaveChanges();
    }
    
	/// <summary>
    ///     Updates record describing an Item.
    /// </summary>
    /// <param name="id">The id of the Item.</param>
    /// <returns>String array</returns>
    /// <response code="200">String array with two values</response>
    /// <response code="400">Error model</response> 
    [HttpPut("{id}")]
    public void Put(int id, [FromBody]Item item)
    {
        var selectedItem = _webAPIDataContext.Items.AsNoTracking().FirstOrDefault(x => x.Id == id);
        if (selectedItem != null)
        {
            _webAPIDataContext.Entry(selectedItem).Context.Update(item);
            _webAPIDataContext.SaveChanges();
        }
    }

	/// <summary>
	///     Delets record describing an Item.
    /// </summary>
	/// <param name="id">The id of the Item.</param>
    /// <response code="200">String array with two values</response>
    /// <response code="400">Error model</response> 
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        var item = _webAPIDataContext.Items.FirstOrDefault(x => x.Id == id);
        if (item != null)
        {
            _webAPIDataContext.Items.Remove(item);
            _webAPIDataContext.SaveChanges();
        }
    }
}