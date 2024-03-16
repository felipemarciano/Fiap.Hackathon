import { Component } from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {FormsModule} from '@angular/forms';
import {MatTableModule} from '@angular/material/table';
import { IListVideosResponseModel } from '../../Constants/Models/videos.models';
import { VideoService } from '../../Services/Http/video.service';
import { SnackbarService } from '../../Services/Components/snackbar.service';
import { FileService } from '../../Services/Files/file.service';


@Component({
  selector: 'app-initial',
  standalone: true,
  imports: [MatButtonModule, 
    MatCheckboxModule, 
    FormsModule, 
    MatTableModule],
  templateUrl: './initial.component.html',
  styleUrl: './initial.component.css'
})

export class InitialComponent {
  fileName: string = 'Nenhum arquivo carregado';
  autoRefresh: boolean = false;
  refreshCounter: number = 0;

  displayedColumns: string[] = ['name', 'startDate', 'startProcessingDate', 'endProcessingDate', 'status', 'download'];
  dataSource: IListVideosResponseModel[] = [];

  constructor(private _videoService: VideoService,
    private _snackbarService: SnackbarService,
    private _fileService: FileService){
  }

  ngOnInit() {
    this.refreshVideoList();
    setInterval(() => { 
        this.refreshVideoList(); 
    }, 5000);
  }

  refreshVideoList(force: boolean = false){
    if (!force && !this.autoRefresh && this.refreshCounter > 0)
      return;

    this._videoService.listVideos().subscribe((data) => {
      this.dataSource = data;
    },
    (error) => {
      console.log(error)
      this._snackbarService.openErrorSnackBar("Erro ao carregar a lista de arquivos");
    });

    this.refreshCounter++;
  }

  onDownloadFile(filePath: string){
    this._videoService.downloadZip(filePath).subscribe((data) => {
      this._fileService.downloadFile(data);
    },
    (error) => {
      console.log(error);
      this._snackbarService.openErrorSnackBar("Erro ao fazer o download do zip");
    }
    );
  }

  onFileSelected(event: any){
    const file: File = event.target.files[0];

    if (!file)
      return;

    this.fileName = file.name;

    var reader = new FileReader();
    reader.readAsDataURL(file);

    reader.onload = () => {
      this._videoService.createVideo({base64Video: reader.result as string, name: file.name})
      .subscribe(
        () => {
          this.refreshVideoList(true);
          this._snackbarService.openSuccessSnackBar("Arquivo carregado com succeso!");
        }, 
        (error) => {
          console.log(error)
          this._snackbarService.openErrorSnackBar("Erro ao carregar o arquivo");
        }
      );
    }
  }
}
