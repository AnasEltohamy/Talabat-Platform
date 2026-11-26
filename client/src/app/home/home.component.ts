import { Component } from '@angular/core';

interface Feature { icon: string; title: string; text: string; }
interface StackGroup { icon: string; title: string; items: string[]; }
interface Step { title: string; text: string; }

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {

  features: Feature[] = [
    {
      icon: 'fa-search',
      title: 'Smart product catalog',
      text: 'Search, filter by brand and type, sort by name or price, and browse through server-side pagination.'
    },
    {
      icon: 'fa-shopping-basket',
      title: 'Persistent basket',
      text: 'Your basket is stored in Redis, so it survives page refreshes and is ready whenever you come back.'
    },
    {
      icon: 'fa-lock',
      title: 'Secure accounts',
      text: 'Register and log in with ASP.NET Identity. Requests are protected with JWT bearer tokens.'
    },
    {
      icon: 'fa-credit-card',
      title: 'Stripe payments',
      text: 'Checkout uses Stripe PaymentIntents. The amount is calculated on the server and confirmed by webhooks.'
    },
    {
      icon: 'fa-history',
      title: 'Order history',
      text: 'Every order is saved with its items, delivery method, address and payment status.'
    },
    {
      icon: 'fa-bolt',
      title: 'Fast responses',
      text: 'Product queries are cached with Redis to keep the shop quick under repeated requests.'
    }
  ];

  steps: Step[] = [
    { title: 'Browse', text: 'Explore the catalog and filter what you like.' },
    { title: 'Add to basket', text: 'Items are saved in your Redis-backed basket.' },
    { title: 'Checkout', text: 'Choose your address and delivery method.' },
    { title: 'Pay', text: 'Pay securely with Stripe.' },
    { title: 'Track', text: 'Review your orders and their status anytime.' }
  ];

  stack: StackGroup[] = [
    {
      icon: 'fa-server',
      title: 'Back-end',
      items: [
        'ASP.NET Core 8 Web API',
        'Onion Architecture',
        'Entity Framework Core',
        'SQL Server',
        'AutoMapper',
        'Repository, Unit of Work & Specification patterns'
      ]
    },
    {
      icon: 'fa-shield',
      title: 'Data & security',
      items: [
        'Redis (basket & caching)',
        'ASP.NET Identity',
        'JWT Bearer authentication',
        'Global exception middleware'
      ]
    },
    {
      icon: 'fa-credit-card',
      title: 'Payments',
      items: [
        'Stripe PaymentIntent',
        'Stripe Elements',
        'Stripe webhooks'
      ]
    },
    {
      icon: 'fa-desktop',
      title: 'Front-end',
      items: [
        'Angular 11',
        'RxJS',
        'Bootstrap 4 (Bootswatch United)',
        'ngx-bootstrap',
        'ngx-toastr & ngx-spinner'
      ]
    }
  ];

  scrollTo(id: string): void {
    const element = document.getElementById(id);
    if (element) {
      element.scrollIntoView({ behavior: 'smooth', block: 'start' });
    }
  }
}