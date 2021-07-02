import { Component, Inject, OnInit } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { ChartOptions, ChartType } from "chart.js";
import { Result } from "src/app/shared/models/result";
import { VotingService } from "src/app/shared/services/user/voting.service";
import { UserViewVote } from "../user-view-vote-dialog/user-view-vote-dialog";
import { SingleDataSet, Label, monkeyPatchChartJsLegend, monkeyPatchChartJsTooltip } from 'ng2-charts';
import * as d3 from "d3";
import { Chart } from "src/app/shared/models/chart";

@Component({templateUrl:'./results-dialog.html'})

export class ResultsDialog implements OnInit{

    results:Result[]=[];
    chart:Chart[]=[];

    
      private svg;
      private margin = 50;
      private width = 600;
      private height = 450;
      // The radius of the pie chart is half the smallest side
      private radius = Math.min(this.width, this.height) / 2 - this.margin;
      private colors;

    constructor(private votingService:VotingService,
        @Inject(MAT_DIALOG_DATA) public data: number,
        private dialogRef:MatDialogRef<ResultsDialog>){
        }

        
        private createSvg(): void {
            this.svg = d3.select("figure#pie")
            .append("svg")
            .attr("width", this.width)
            .attr("height", this.height)
            .append("g")
            .attr(
              "transform",
              "translate(" + this.width / 2 + "," + this.height / 2 + ")"
            );
        }

        private createColors(res:Result[]): void {

            this.colors = d3.scaleOrdinal()
            .domain(res.map(d=>d.voteCount.toString()))
            .range(["#c7d3ec", "#a5b8db", "#879cc4"]);
        }

        private drawChart(res:Result[]): void {
            // Compute the position of each group on the pie:
            const pie = d3.pie<any>().value((d: any) => Number(d.voteCount));
        
            // Build the pie chart
            this.svg
            .selectAll('pieces')
            .data(pie(res))
            .enter()
            .append('path')
            .attr('d', d3.arc()
              .innerRadius(0)
              .outerRadius(this.radius)
            )
            .attr('fill', (d, i) => (this.colors(i)))
            .attr("stroke", "#121926")
            .style("stroke-width", "1px");
        
            // Add labels
            const labelLocation = d3.arc()
            .innerRadius(100)
            .outerRadius(this.radius);
        
            this.svg
            .selectAll('pieces')
            .data(pie(res))
            .enter()
            .append('text')
            .text(d=>d.data.candidateName)
            .attr("transform", d => "translate(" + labelLocation.centroid(d) + ")")
            .style("text-anchor", "middle")
            .style("font-size", 15);
        }


    ngOnInit(){

        this.seeResults();
       

    }

    seeResults(){

        console.log(this.data);

        this.votingService.getResults(this.data).toPromise()
        .then(res=>{     
          this.results=res;
          let r=res.filter(el=>el.voteCount!=0)
          this.createSvg();
          this.createColors(r);
          this.drawChart(r);
        })
        .catch(err=>{
          console.log(err);
        });
    }
}