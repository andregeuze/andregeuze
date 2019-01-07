# We use php 7.1.8 as base image
FROM php:7.1.8-apache

COPY ./src .
COPY vhost.conf /etc/apache2/sites-available/000-default.conf

RUN service apache2 restart