import { Injectable } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';

import { _EntityBase } from './_EntityBase';
import { Person } from './Person';
import { DocumentType } from './DocumentType';

@Injectable()
export class Document extends _EntityBase {
  public IdDocumentType: number;
  public FKDocumentType: DocumentType;
  public Value: string;
  public IdPerson: number;
  public FKPerson: Person;

  constructor() {
    super();
    this.FKPerson = new Person();
  }
}
