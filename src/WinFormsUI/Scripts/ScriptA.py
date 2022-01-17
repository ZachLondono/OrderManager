import clr
clr.AddReference("ApplicationCore");
from OrderManager.ApplicationCore.Domain import LineItem 

def Script(args):

	height = args.Attributes["Height"]
	width = args.Attributes["Width"]
	depth = args.Attributes["Depth"]

	return "Height %s x Width %s x Depth %s" % (height, width, depth)
