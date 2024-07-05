import { TestBed } from '@angular/core/testing';

import { ServicesApiProxyService } from './services-api-proxy.service';

describe('ServicesApiProxyService', () => {
  let service: ServicesApiProxyService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ServicesApiProxyService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
