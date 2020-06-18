from django.shortcuts import render
from django.http import HttpResponse
from datetime import datetime
# Create your views here.

def index(request):
    now = datetime.now()
    #html_content = "<html><head><title>Hello, Django</title></head><body>"
    #html_content += "<strong>Hello Django!</strong> on " + now.strftime("%A, %d %B, %Y at %X")
    #html_content += "</body></html>"
    #return HttpResponse(html_content)
    return render(request, "Django/index.html",
                  {
                      'title' : "Django",
                      'message' : "Hello Django!",
                      'content' : " on " + now.strftime("%A, %d %B, %Y at %X")
                  })