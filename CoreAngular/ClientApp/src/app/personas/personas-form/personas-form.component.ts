import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormArray } from '@angular/forms';
import { IPersona } from '../persona';
import { PersonasService } from '../personas.service';
import { Router, ActivatedRoute } from '@angular/router';
import { DeprecatedDatePipe, DatePipe } from '@angular/common';
@Component({
  selector: 'app-personas-form',
  templateUrl: './personas-form.component.html',
  styleUrls: ['./personas-form.component.css']
})
export class PersonasFormComponent implements OnInit {

  constructor(private fb: FormBuilder,
    private personasService: PersonasService,
    private router: Router,
    private activateRoute: ActivatedRoute) { }

  //------Variables
  modoEdicion: boolean = false;
  formGroup: FormGroup;
  personaId: number;
  direccionesABorrar:number[]=[];
  ngOnInit() {
    this.formGroup = this.fb.group({
      nombre: '',
      fechaNacimiento: '',
      direcciones: this.fb.array([])
    })

    this.activateRoute.params.subscribe(params => {
      if (params["id"] == undefined) {
        return;
      }
      this.modoEdicion = true;
      this.personaId = params["id"];

      //Api id de persona para edición
      this.personasService.listPersonabyId(this.personaId.toString())
        .subscribe(persona => this.cargaFormulario(persona)
          , error => this.router.navigate(["/personas"]));

    });

  }

  agregarDireccion() {
    let direccionArr = this.formGroup.get('direcciones') as FormArray;
    let direccionFG = this.construirDireccion();
    direccionArr.push(direccionFG);
  }

  //crear metodo para crear direccion
  construirDireccion() {
    return this.fb.group({
      id: 0,
      calle: '',
      provincia: '',
      personaId: this.personaId != null ? this.personaId : 0

    });
  }

  //remover el arreglo de direccion dinamicas con el boton
  removerDireccion(index: number){
    let direcciones=this.formGroup.get('direcciones') as FormArray;
    let direccionRemover=direcciones.at(index) as FormGroup;
    if (direccionRemover.controls['id'].value!='0'){
     this.direccionesABorrar.push(<number>direccionRemover.controls['id'].value);
    }
    direcciones.removeAt(index);
  }

  //cargar el formulario con los datos del usuario
  cargaFormulario(persona: IPersona) {
    var dp = new DatePipe(navigator.language);
    var formato = "yyyy-MM-dd";
    this.formGroup.patchValue({
      nombre: persona.nombre,
      fechaNacimiento: dp.transform(persona.fechaNacimiento, formato)
    });

    let direcciones = this.formGroup.controls['direcciones'] as FormArray;
    persona.direcciones.forEach(direccion=>{
     let direccionFG=this.construirDireccion();
     direccionFG.patchValue(direccion);
     direcciones.push(direccionFG);

    });

  }


  save() {

        let persona: IPersona = Object.assign({}, this.formGroup.value)
    console.table(persona);

    if (this.modoEdicion) {
      //editar registro
      persona.id = this.personaId;
      this.personasService.ActualizarPersona(persona)
        .subscribe(persona => this.Ok()
          , error => console.error(error));
    } else {
      //Agregar registro
      this.personasService.crearPersona(persona)
        .subscribe(persona => this.Ok()
          , error => console.error(error));
    }


  }

  //metodo respuesta desde API
  Ok() {
    this.router.navigate(["/personas"]);
  }
}
