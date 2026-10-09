import { TestBed } from '@angular/core/testing';
import { Cdb } from './cdb';

describe('Cdb', () => {
  let service: Cdb;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Cdb);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
