# API REFERENCE

# Orders
This objects represents a placed order. You can retrieve it to see information about its current status, and the companies involved in the order.

### <u>The Order Object</u>
``` javascript
{
	“id” : 0,
	“name” : "Kitchen A",
	“number” : "OT000",
	“customer” : {
		"id" :  1,
		"name” : "Joe's Cabinet Shop"
	},
	“vendor” : {
		"id" :  2,
		"name” : "Metro Cabinet Parts"
	},
	“supplier” : {
		"id" :  3,
		"name” : "Royal Cabinet Company"
	},
	“status” : "Pending",
	“placed_date” : 5-18-22,
	“completion_date” : null,
	“confirmation_date” : null,
	“last_modified_date” : null,
	“info” : {
		"comment" : "My first order"
	},
	"items" : [
		1,2,3
	]
}
```
### <u>Attributes</u>
__id__ <small>positive integer</small>
 Unique identifier for the order
 
 __Name__ <small>string</small>
 Customer supplied reference name for the order
 
 __Number__ <small>string</small>
Vendor supplied reference number that should be unique for the given vendor

__Customer__ <small>object</small>
Company which is purchasing the products

 __Vendor__ <small>object</small>
Company which sold the order

 __Supplier__ <small>object</small>
Company which is manufacturing the order

__Status__ <small>string</small>
Current status of the order; <i>pending, confirmed, completed, void</i>

__Placed Date__ <small>date</small>
Date when order was originally placed

__Confirmed Date__ <small>date</small>
Date when order was confirmed

__Completion Date__ <small>date</small>
Date when order was marked as completed

__Last Modified Date__ <small>date</small>
Date when any part of the order was last changed

__Info__ <small>dictionary</small>
Arbitrary fields associated with the order. May include, but is not limited to, comments by the customer or vendor, notes from a user, etc.

__Items__ <small>array</small>
List of ids of the items in the order

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
Returns a paginated list of order objects.
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
Returns the details of an order which has previously been placed. Provide the order ID returned when a previous request.
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
Updates the status of an order to confirmed, and ready to be manufactured.
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

# Order Items
_todo write description_

### <u>The OrderedItem Object</u>
``` javascript
{
	“id” : 0,
}
```
### <u>Attributes</u>
__id__ <small>positive integer</small>
 Unique identifier for the ordered item

### <u>Endpoints<u>

| Method      | Endpoint                              | Action
| ----------- | -----------                           | -------
| GET         | /api/sales/orders/:id/items           | Retrieve a list of items in an order
| POST        | /api/sales/orders/:id/items           | Add an item to an order
| POST        | /api/sales/orders/:id/items/:item_id  | Update an ordered item
| DELETE      | /api/sales/orders/:id/items/:item_id  | Removes an item from an order

_todo describe endpoints_ 

# Companies
_todo write description_

### <u>The Company Object</u>
``` javascript
{
	“id” : 0,
}
```
### <u>Attributes</u>
__id__ <small>positive integer</small>
 Unique identifier for the company

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
_todo write description_

### <u>The Product Object</u>
``` javascript
{
	“id” : 0,
}
```
### <u>Attributes</u>
__id__ <small>positive integer</small>
 Unique identifier for the product

### <u>Endpoints<u>

| Method      | Endpoint                          | Action
| ----------- | -----------                       | -------
| GET         | /api/catalog/products/            | Retrieve a list of companies
| GET         | /api/catalog/products/:id         | Retrieve a specific company
| POST        | /api/catalog/products             | Create a new company
| POST        | /api/catalog/products/:id         | Update an existing company

_todo describe endpoints_ 

# Jobs
_todo write description_

### <u>The Job Object</u>
``` javascript
{
	“id” : 0,
}
```
### <u>Attributes</u>
__id__ <small>positive integer</small>
 Unique identifier for the job

### <u>Endpoints<u>

| Method      | Endpoint                                   | Action
| ----------- | -----------                                | -------
| GET         | /api/manufacturing/jobs/                   | Retrieve a list of jobs
| GET         | /api/manufacturing/jobs/:id                | Retrieve a specific job
| POST        | /api/manufacturing/jobs/:id/schedule       | Schedule a job
| POST        | /api/manufacturing/jobs/:id/release        | Mark a job as released
| DELETE      | /api/manufacturing/jobs/:id/complete       | Mark a job as complete
| DELETE      | /api/manufacturing/jobs/:id/ship           | Mark a job as shipped
| DELETE      | /api/manufacturing/jobs/:id/cancel         | Mark a job as canceled

_todo describe endpoints_ 
