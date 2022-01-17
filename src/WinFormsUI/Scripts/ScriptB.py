from System.Collections.Generic import List

class Product(object):
	LineNumber = ""
	Name = ""

class DataModel(object):
	Title = ""
	Products = "" #List[Product]()

def Script(args):
	prod1 = Product()
	prod1.LineNumber = "1"
	prod1.Name = "Front/Back"

	prod2 = Product()
	prod2.LineNumber = "1"
	prod2.Name = "Sides"

	prod3 = Product()
	prod3.LineNumber = "2"
	prod3.Name = "Front/Back"

	prod4 = Product()
	prod4.LineNumber = "2"
	prod4.Name = "Sides"

	#products = List[Product]()

	model = DataModel()
	model.Title = "This is the title"
	#model.Products = products

	return model
