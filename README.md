# Webshop

This repository is my take on a webshop. I have built and modified many webshops in the past for big customers. Now I want to build my own and take you guys with me.

Follow my journey on [dev.to](https://dev.to/suneeh).

# Contributing

## Clone the repo

Get the code of this git repository to your machine.

`git clone https://github.com/Suneeh/webshop.git`

## Install dependancies

Use `npm install` to install all packages and dependancies.

## Development server

Copy the `./auth_config.example.json` [file](https://github.com/Suneeh/webshop/blob/main/frontend/auth_config.example.json) and remove the `example` fill it with your own values. After that run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The application will automatically reload if you change any of the source files.

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
  - [x] ~~Create a company logo~~
    - [x] ~~Add the company logo to the sidebar~~
    - [x] ~~Add FavIcon~~
    - [x] ~~Add Logo to the login mask of auth0~~
- ~~Product Rating~~
  - [x] ~~Build Component that shows stars instead of numbers~~
  - [x] ~~Call Action Data Source to actually rate an item~~
- Category Page
  - [ ] Build Sorting & Filtering (once there is metrics to sort/filter by)
  - [ ] Build Pagination (will this ever be needed ?)
- Product-List Component
  - [x] Show Rating
  - [x] Show Discount
- Product-Detail Page
  - [x] Build stub with link and route working
  - [x] Show all the information on the page
  - [x] Rating(s) of users
  - [x] Discount
  - [ ] Style the page
  - [ ] Think about ideas of how to fill the page afterwards
    - Related Products
    - Items in Stock ?
    - Tags ?
- Dashboard
  - [x] Newest products
  - [ ] Top Rated Products
  - [ ] Build Landing Page
  - [ ] Top selling products (super late - when orders are a thing)
  - [ ] Special offer products
- Misc
  - [x] Refactor API to Seperate APIs / Files with 1 file for Endpoint + Method + DTOs
  - [x] Build Css with Media Queries for Mobile, FullHD and QHD screens
  - [ ] Write Test(s) for the endpoints
  - [ ] Check why intellisense is not suggesting imports correctly?
  - [ ] Brand categories?
