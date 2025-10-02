import { Component, OnInit, effect, input } from '@angular/core';
import { FormArray, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CONFIG_EDITOR_CONTROL_NAMES } from '../shared/config-editor-control-names';
import { EditableInputComponent } from "../components/editable-input/editable-input.component";
import { CONFIG_EDITOR_CONTROL_LABELS } from '../shared/config-editor-control-labels';

@Component({
  selector: 'config-editor-routes',
  imports: [
    ReactiveFormsModule,
    EditableInputComponent
  ],
  templateUrl: './routes.component.html',
  styleUrls: [
    './routes.component.css',
    '../config-editor.component.css'
  ]
})
export class RoutesComponent implements OnInit {

  public readonly CONTROL_NAMES = CONFIG_EDITOR_CONTROL_NAMES;
  public readonly CONTROL_LABELS = CONFIG_EDITOR_CONTROL_LABELS;
  public readonly parent = input.required<FormGroup<any> | null>();
  readonly routesArrayName = input.required<string>();
  public readonly clusterNameChangedTrigger = input<{ clusterNameBeforeChange: string; newClusterName: string } | null>();
  public parentFormGroup!: FormGroup;
  public routes!: FormArray;

  constructor() {
    effect(() => {
      const trigger = this.clusterNameChangedTrigger();
      if (trigger) {
        this.changeClusterName(trigger.clusterNameBeforeChange, trigger.newClusterName);
      }
    });
  }

  ngOnInit(): void {
    this.parentFormGroup = this.parent() as FormGroup;
    this.routes = this.parentFormGroup.get(this.routesArrayName()) as FormArray;
  };

  private changeClusterName(oldClusterName: string, newClusterName: string): void {
    this.routes.controls.forEach(routeControl => {
      const route = routeControl as FormGroup;
      const routeName = route.get(this.CONTROL_NAMES.ROUTE_NAME)?.value;
      const clusterNameControl = route.get(this.CONTROL_NAMES.CLUSTER_NAME);
      const currentClusterName = clusterNameControl?.value;
      const isChangeRequired = currentClusterName === oldClusterName;
      if (routeName && isChangeRequired) {
        clusterNameControl?.setValue(newClusterName);
        this.notifyUserClusterNameChanged(routeName, oldClusterName, newClusterName);
        // TODO: Replace alert with a better user notification system
        // TODO: Make request to backend to update all routes with the old cluster name to the new cluster name
      }
    });
  }

  notifyUserClusterNameChanged(routeName: any, oldClusterName: string, newClusterName: string) {
    const message = `The cluster name for route "${routeName}" has been updated from "${oldClusterName}" to "${newClusterName}".`;
    alert(message);
  }
}
