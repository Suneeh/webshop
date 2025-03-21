# Webshop

This repository is my take on a webshop. I have built and modified many webshops in the past for big customers. Now I want to build my own and take you guys with me.

Follow my journey on [dev.to](https://dev.to/suneeh).

Check it out! It is deployed [here](https://shop.suneeh.de/).

# Contributing

## Clone the repo

Get the code of this git repository to your machine.

`git clone https://github.com/Suneeh/webshop.git`

## Install dependancies

Use `npm install` to install all packages and dependancies.

## Development server

### Local

Copy the `./auth_config.example.json` [file](https://github.com/Suneeh/webshop/blob/main/frontend/auth_config.example.json) and remove the `example` fill it with your own values. After that run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The application will automatically reload if you change any of the source files.

### Docker

#### Frontend Docker Start Up

`docker build -t frontend . && docker run -d -p 80:80 --name frontend frontend`

#### Backend Docker Start Up

`docker build -t backend . && docker run -d -p 5292:8080 --name backend backend`

#### Database Docker Start Up

`docker run --name webshopDb -e POSTGRES_PASSWORD=MYPASSWORD -p 5432:5432 -d postgres`

## Deployment

Run `npm run deploy` after you renamed the `deployment.exmaple.sh` to `deployment.sh`. Also be sure to adjust this to your needs as shown in the [example](https://github.com/Suneeh/webshop/blob/main/deploy.example.sh).

# TODOs

- Auth
  - [ ] Build a login mask instead of redirecting to Auth0
  - [x] ~~Login~~
  - [x] ~~Add auth to the Backend~~
  - [x] ~~Use Auth correctly~~
- General
  - [ ] (Future) Add guards for user specifics or management pages
- Shell
  - [ ] Build Footer with hotlinks and more information
  - [ ] Create a company logo
    - [ ] Add the company logo to the sidebar
    - [ ] Add FavIcon
- Category Page
  - [x] Description
  - [x] Products
  - [x] Build Product Card with color + price(s) -> Call to Action Button redirects to detail page
  - [ ] Build Sorting
  - [ ] Build Filtering
  - [ ] Build Pagination ?
- Product-List Page
  - [x] Build Component with the most important Data available
  - [x] Style Component (Color code should translate into an "image" box of that color)
  - [x] Add Db Migration to store color codes
  - [x] Fetch color codes from API
- Product-Detail Page
  - [x] Build stub with link and route working
  - [ ] Show all the information on the page
  - [ ] Style the page
  - [ ] Think about ideas of how to fill the page afterwards
    - Related Products
    - Discount ?
    - Items in Stock ?
    - Tags ?
    - Rating(s) of users
- Dashboard

  - [x] Newest products
  - [ ] Build Landing Page
  - [ ] Top selling products (super late - when orders are a thing)
  - [ ] Special offer products

- Api Client
  - [x] Build a Frontend Api Client that calls the backend correctly
- Misc
  - [x] Add linting
  - [x] Update to Ng19
  - [x] Set locale so prices are shown correctly
  - [ ] Write Tests for the API(s)
  - [ ] Check why intellisense is not suggesting imports correctly?
  - [ ] Build a public API instead of exporting in each file?
  - [ ] Brand categories?
