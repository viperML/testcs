{
  inputs = {
    nixpkgs.url = "github:NixOS/nixpkgs/nixos-unstable";
    flake-parts.url = "github:hercules-ci/flake-parts";
  };

  outputs = inputs @ {flake-parts, ...}:
    flake-parts.lib.mkFlake {inherit inputs;} {
      systems = [
        "x86_64-linux"
      ];

      perSystem = {pkgs, ...}: {
        devShells.default = with pkgs; let
          myDotnet = dotnetCorePackages.sdk_7_0;
        in
          mkShellNoCC {
            packages = [
              myDotnet
              (nuget-to-nix.override {dotnet-sdk = myDotnet;})
            ];
          };
      };
    };
}
