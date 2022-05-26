# API REFERENCE

# Orders
This objects represents a placed order. You can retrieve it to see information about its current status, and the companies involved in the order.

### <u>The Order Object</u>
``` javascript
{
	"id" : 1,
	"name" : "Kitchen A",
	"number" : "OT000",
	"customer" : {
		"id" :  1,
		"name" : "Joe's Cabinet Shop"
	},
	"vendor" : {
		"id" :  2,
		"name" : "Metro Cabinet Parts"
	},
	"supplier" : {
		"id" :  3,
		"name" : "Royal Cabinet Company"
	},
	"status" : "confirmed",
	"placed_date" : 5-18-22,
	"completion_date" : null,
	"confirmation_date" : null,
	"last_modified_date" : null,
	"info" : {
		"comment" : "My first order"
	},
	"items" : [
		{...}
	]
}
```
### <u>Attributes</u>
__id__ <small>positive integer</small>
 Unique identifier for the order
 
 __name__ <small>string</small>
 Customer supplied reference name for the order
 
 __number__ <small>string</small>
Vendor supplied reference number that should be unique for the given vendor

__customer__ <small>object</small>
Company which is purchasing the products

 __vendor__ <small>object</small>
Company which sold the order

 __supplier__ <small>object</small>
Company which is manufacturing the order

__status__ <small>string</small>
Current status of the order; <i>pending, confirmed, completed, void</i>

__placed_date__ <small>date</small>
Date when order was originally placed

__confirmation_date__ <small>date</small>
Date when order was confirmed

__completed_date__ <small>date</small>
Date when order was marked as completed

__last_modified_date__ <small>date</small>
Date when any part of the order was last changed

__info__ <small>dictionary</small>
Arbitrary fields associated with the order. May include, but is not limited to, comments by the customer or vendor, notes from a user, etc.

__items__ <small>array</small>
List of ordered items (see ordered items section)

### <u>Endpoints<u>

| Method      | Endpoint                             | Action
| ----------- | -----------                          | -------
| GET         | /api/sales/orders                    | Retrieve a list of orders
| GET         | /api/sales/orders/:id                | Retrieve a specific order
| POST        |  /api/sales/orders                   | Place an order
| POST        |  /api/sales/orders/:id               | Update an order
| POST        |  /api/sales/orders/:id/confirm       | Mark an order as confirmed
| POST        |  /api/sales/orders/:id/complete      | Mark an order as completed
| POST        |  /api/sales/orders/:id/void          | Mark an order as voided


- __Retrieve a list of orders__  <small>get : /api/sales/orders</small>
Returns a paginated list of order objects. This order object will not include the ordered items.
<b>Parameters</b>
<u>limit</u> _<small>optional</small>_
A limit on the number of objects to return in the list. The default is 10.
<u>page</u> _<small>optional</small>_
The index of the page to return. The default is 0
<u>sort</u> _<small>optional</small>_
Determines the order of the objects returned by the query; _placed_date, modified_date, number, name_
<u>filter</u> _<small>optional</small>_
Filter the objects returned; _customer, vendor, supplier_


- __Retrieve an order__   <small>get : /api/sales/orders/:id</small>
Returns the details of an order which has previously been placed, including the items in the order. Provide the order ID returned when a previous request.
<b>Parameters</b>
_no parameters_

 - __Place an order__   <small>post : /api/sales/orders</small>
Creates a new order with the provided information
<b>Parameters</b>
_todo write description_

 - __Update an order__   <small>post : /api/sales/orders/:id</small>
Updates an order with the provided order ID
<b>Parameters</b>
_todo write description_

 - __Mark an order as confirmed__   <small>post : /api/sales/orders/:id/confirm</small>
Updates the status of an order to confirmed, and ready to be manufactured. When an order is confirmed, it is locked from further editing.
<b>Parameters</b>
_no parameters_
 - __Mark an order as completed__   <small>post : /api/sales/orders/:id/complete</small>
Updates the status of an order to completed, it has been manufactured and shipped. 
<b>Parameters</b>
_no parameters_
 - __Mark an order as void__   <small>post : /api/sales/orders/:id/void</small>
Updates the status of an order to void, no longer valid.
<b>Parameters</b>
_no parameters_

# Ordered Items
An ordered item represents an item which makes up an order.

### <u>The OrderedItem Object</u>
``` javascript
{
	"id" : 1,
	"oreder_id" : 1,
	"product_id" : 1,
	"product_name" : "Dovetail Drawer Box",
	"line" : 1,
	"qty" : 2,
	"options" : {
		"height" : "4.125",
		"width" : "21",
		"depth" : "21"
	}
}
```
### <u>Attributes</u>
__id__ <small>positive integer</small>
 Unique identifier for the ordered item
 
 __product_id__ <small>positive integer</small>
 Unique identifier for the product that was ordered
  
 __product_name__ <small>string</small>
 Name of the product that was ordered
  
 __line__ <small>positive integer</small>
The position of this item in the order

 __qty__ <small>positive integer</small>
The quantity ordered of this item

 __options__ <small>dictionary</small>
Key value pairs which represent the options and their values for this item 
 

### <u>Endpoints<u>

| Method      | Endpoint                                  | Action
| ----------- | -----------                               | -------
| POST        | /api/sales/orders/:order_id/items         | Add an item to an order
| POST        | /api/sales/orders/:order_id/items/:item_id| Update an ordered item
| DELETE      | /api/sales/orders/:order_id/items/:item_id| Removes an item from an order

- __Add an item to an order__  <small>post : /api/sales/orders/:order_id/items</small>
Adds a new item to an existing order

- __Update an item in an order__  <small>post : /api/sales/orders/:order_id/items/:item_id</small>
Updates an existing item in an existing order

- __Remove an item from an order__  <small>delete : /api/sales/orders/:order_id/items/:item_id</small>
Removes an exiting ordered item from an existing order

# Companies
A company has at least one of three roles; _customer, vendor, or supplier_. 

### <u>The Company Object</u>
``` javascript
{
	"id" : 1,
	"name" : "Royal Cabinet Company",
	"email" : "zach@royalcabinet.com",
	"roles" : ["customer","vendor","supplier"],
	"address" : {
		"line1" : "",
		"line2" : "",
		"line3" : "",
		"city" : "",
		"state" : "",
		"zip" : ""
	}
}
```
### <u>Attributes</u>
__id__ <small>positive integer</small>
 Unique identifier for the company
 
 __name__ <small>string</small>
 The companies name
 
 __roles__ <small>array</small>
 A list of the companies roles
 
 __address__ <small>object</small>
The companies address

### <u>Endpoints<u>

| Method      | Endpoint                         | Action
| ----------- | -----------                      | -------
| GET         | /api/sales/companies/            | Retrieve a list of companies
| GET         | /api/sales/companies/:id         | Retrieve a specific company
| POST        | /api/sales/companies             | Create a new company
| POST        | /api/sales/companies/:id         | Update an existing company
| DELETE      | /api/sales/companies/:id         | Remove an existing company

_todo describe endpoints_ 

# Products
Products are items which make up an order. 

### <u>The Product Object</u>
``` javascript
{
	"id" : 1,
	"name" : "Dovetail Drawer Box",
	"attributes" : {
		"Height" : "0",
		"Width" : "0",
		"Depth" : "0"
	}
}
```
### <u>Attributes</u>
__id__ <small>positive integer</small>
 Unique identifier for the product
 
 __name__ <small>string</small>
 The name of the product
 
 __attributes__ <small>dictionary</small>
Key value pair dictionary of all the product's options mapped to their default values

### <u>Endpoints<u>

| Method      | Endpoint                   | Action
| ----------- | -----------                | -------
| GET         | /api/catalog/products/     | Retrieve a list of products
| GET         | /api/catalog/products/:id  | Retrieve a specific product
| POST        | /api/catalog/products      | Create a new product
| PUT		  | /api/catalog/products/     | Update an existing product
| DELETE	  | /api/catalog/products/:id  | Removes a product

- __Retrieve a list of products__  <small>get : /api/catalog/products/</small>
Retrieves a list of all products in the catalog. The response only includes the name and ID of the products.
__Parameters__
_no parameters_

__Response__
``` javascript
[
	{
		"id" : 1,
		"name" : "Dovetail Drawer Box"
	},
	{
		"id" : 2,
		"name" : "MDF Door"
	},
	{...}
]
```

- __Retrieve a specific products__  <small>get: /api/catalog/products/:id</small>
Retrieves the details of a specific product. Includes the ID, name and attributes.
__Parameters__
_no parameters_

__Response__
``` javascript
{
	"id" : 1,
	"name" : "Dovetail Drawer Box",
	"attributes" : {
		"Height" : "0",
		"Width" : "0", 
		"Depth" : "0"
	}
}
```

- __Create a new products__  <small>post : /api/catalog/products/</small>
Retrieves the details of a specific product. Includes the ID, name and attributes.
__Parameters__
<u>name</u> _<small>required</small>_
The name of the new product
<u>attributes</u> _<small>optional</small>_
A dictionary representing the new product's attributes and their default values.

- __Create a new products__  <small>put : /api/catalog/products/</small>
Retrieves the details of a specific product. Includes the ID, name and attributes.
__Parameters__
<u>id</u> _<small>required</small>_
The existing product's ID
<u>name</u> _<small>option</small>_
The new name of the product
<u>attributes</u> _<small>optional</small>_
A dictionary representing the new product's attributes and their default values.

# Jobs
A job represents work that must be done to manufacture some products in an order. A single order may have one or more jobs associated with it. 

### <u>The Job Object</u>
``` javascript
{
	"id" : 1,
	"order_id" : 1,
	"name" : "Kitchen A",
	"number" : "OT000",
	"customer" : "Joe's Cabient Shop",
	"status" : "in-progress",
	"completed_date" : null,
	"products" : {
		1 : 2
	}
}
```



### <u>Attributes</u>
__id__ <small>positive integer</small>
 Unique identifier for the job
 
 __order_id__ <small>positive integer</small>
 ID of the order this job is associated with
  
__name__ <small>string</small>
The name of the order this job is associated with

__number__ <small>string</small>
The number of the order this job is associated with

__customer__ <small>string</small>
The customer who ordered 

__status__ <small>string</small>
Current status of the job; _pending, in-progress, complete, canceled_

__products__ <small>dictionary</small>
Dictionary mapping product IDs to the quantity of each product in the job

### <u>Endpoints<u>

| Method      | Endpoint                                   | Action
| ----------- | -----------                                | -------
| GET         | /api/manufacturing/jobs/                   | Retrieve a list of jobs
| GET         | /api/manufacturing/jobs/:id                | Retrieve a specific job
| POST      | /api/manufacturing/jobs/:id/complete       | Mark a job as complete
| POST      | /api/manufacturing/jobs/:id/cancel         | Mark a job as canceled

_todo describe endpoints_ 

# Work Cell
A work cell represents an arrangement of resources which is able to produce one or more products. Each work cell has an estimated maximum output. A __Job__ is assigned to a work cell and given a scheduled completion date.

### <u>The Work Cell Object</u>
``` javascript
{
	"id" : 1,
	"alias" : "Dovetail Cell A",
	"output" : {
		{
			"prouct_id" : 1,
			"daily_output" : 50
		}
	},
	"active_jobs" : [
		{
			"job_id" : 1,
			"scheduled_date" : "05-25-2022"
		},
		{ ... }
	]
}
```

### <u>Endpoints<u>

| Method      | Endpoint                                    | Action
| ----------- | -----------                                 | -------
| GET         | /api/manufacturing/cells/                   | Retrieve a list of work cells
| GET         | /api/manufacturing/cells/:id                | Retrieve a specific work cell
| POST        | /api/manufacturing/cells/			        | Create a work cell
| POST        | /api/manufacturing/cells/:id				| Update a work cell
| POST        | /api/manufacturing/cells/:id/schedule       | Schedule a job
| DELETE      | /api/manufacturing/cells/:id			    | Delete a work cell
| DELETE      | /api/manufacturing/cells/:cell_id/:job_id   | Remove a job from a work cell

- __Schedule a Job__  <small>post : /api/manufacturing/cell/:id/schedule</small>
Schedules a job to be completed by the given __Work Cell__ on a given date. If the job already exists in the __Work Cell__ it's scheduled date will be updated. If the job already exists in another __Work Cell__ it will be removed before being added to the new one. If the job is not already assigned to a __Work Cell__ it will be removed from the __Back Log__ before being added.
__Parameters__
<u>job_id</u> _<small>required</small>_
The ID of the job to add to the work cell
<u>scheduled_date</u> _<small>required</small>_
The date that the job is scheduled to be completed



# Backlog
A backlog is a list of orders which have not been assigned a __Work Cell__. Once an __Order__ is released, all of it's associated __Jobs__ will be added to a __Back Log__, making them available to be assigned to a __Work Cell__. There is 1 backlog for each __Product__ in the catalog, when a product is created a new __Back Log__ is created.

### <u>The Backlog Object</u>
``` javascript
{
	"id" : 1,
	"product_id" : 1,
	"jobs" : [
		{
			"id" : 1,
			"number" : "OT000",
			"qty" : 5 
		},
		{ ... }
	]
}
```
| Method      | Endpoint                                   | Action
| ----------- | -----------                                | -------
| GET         | /api/manufacturing/backlogs/                   | Retrieve a list of back logs
| GET         | /api/manufacturing/backlogs/:id                | Retrieve a specific back log
