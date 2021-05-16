with open("images/10-sm.jpg", "rb") as image:
  f = image.read()
  b = bytearray(f)
  
print (b)