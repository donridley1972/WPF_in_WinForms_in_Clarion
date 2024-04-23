  MEMBER('WpfInWinFormsInClarion.clw')

  PRAGMA('define(init_priority=>3)')

  MAP
    MODULE('WpfInWinFormsInClarion_BC0.CLW')
WpfInWinFormsInClarion_BC0:DctInit             PROCEDURE()
WpfInWinFormsInClarion_BC0:DctKill             PROCEDURE()
WpfInWinFormsInClarion_BC0:FilesInit           PROCEDURE()
    END
  END

DctInit PROCEDURE()
  CODE
  WpfInWinFormsInClarion_BC0:DctInit
  WpfInWinFormsInClarion_BC0:FilesInit


DctKill PROCEDURE()
  CODE
  WpfInWinFormsInClarion_BC0:DctKill

