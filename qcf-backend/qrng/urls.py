from django.urls import path
from django.urls import re_path as url

from qrng import views

urlpatterns = [
    path('',views.home),
    url('^backends/$', views.backends),
    url('^results/$', views.results),
    url('^backends/least/$', views.leastbusy)
    ]